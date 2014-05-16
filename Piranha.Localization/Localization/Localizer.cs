using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace Piranha.Localization
{
	public static class Localizer
	{
		/// <summary>
		/// Localizes the client page model depending on the current UI culture.
		/// </summary>
		/// <param name="model">The page model</param>
		public static void LocalizePageModel(Piranha.Models.PageModel model) {
			var def = Utils.GetDefaultCulture();
			
			//
			// Check that we have a culture other than the default culture
			//
			if (def.Name != CultureInfo.CurrentUICulture.Name) {
				var js = new JavaScriptSerializer();

				using (var db = new Db()) {
					var translation = db.PageTranslations
						.Include(p => p.Regions)
						.Where(p => p.PageId == model.Page.Id)
						.SingleOrDefault();

					// Map page values
					((Piranha.Models.Page)model.Page).Title = translation.Title;
					((Piranha.Models.Page)model.Page).NavigationTitle = translation.NavigationTitle;
					((Piranha.Models.Page)model.Page).Keywords = translation.Keywords;
					((Piranha.Models.Page)model.Page).Description = translation.Description;

					// Map regions
					foreach (var reg in translation.Regions) {
						var template = Piranha.Models.RegionTemplate.GetSingle(reg.TemplateId);
						if (template != null) {
							var internalId = template.InternalId;
							var type = Extend.ExtensionManager.Current.GetType(reg.Type);
							object val = null;

							if (typeof(IHtmlString).IsAssignableFrom(type)) {
								val = new HtmlString(reg.Body);
							} else {
								val = js.Deserialize(reg.Body, type);

								val = ((Extend.IExtension)val).GetContent(model);
							}
							((IDictionary<string, object>)model.Regions)[internalId] = val;
						}
					}
				}
			}
		}

		/// <summary>
		/// Loads the localized content depending on the current UI culture
		/// </summary>
		/// <param name="model">The page edit model</param>
		public static void LocalizePageOnLoad(Piranha.Models.Manager.PageModels.EditModel model) {
			var def = Utils.GetDefaultCulture();
			
			//
			// Check that we have a culture other than the default culture
			//
			if (def.Name != CultureInfo.CurrentUICulture.Name) {
				var js = new JavaScriptSerializer();

				using (var db = new Db()) {
					var translation = db.PageTranslations
						.Include(p => p.Regions)
						.Where(p => p.PageId == model.Page.Id)
						.SingleOrDefault();

					if (translation != null) {
						// Map page values
						model.Page.Title = translation.Title;
						model.Page.NavigationTitle = translation.NavigationTitle;
						model.Page.Keywords = translation.Keywords;
						model.Page.Description = translation.Description;

						for (var n = 0; n < model.Regions.Count; n++) {
							var region = model.Regions[n];

							// Get the translated region
							var reg = translation.Regions
								.Where(r => r.RegionId == region.Id && r.IsDraft == region.IsDraft)
								.SingleOrDefault();
							if (reg != null) {
								if (region.Body is IHtmlString) {
									region.Body = new Piranha.Extend.Regions.HtmlRegion(reg.Body);
								} else {
									region.Body = (Piranha.Extend.IExtension)js.Deserialize(reg.Body, region.Body.GetType());
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Stores the localized content depending on the current UI culture.
		/// </summary>
		/// <param name="model">The page edit model</param>
		public static void LocalizePageBeforeSave(Piranha.Models.Manager.PageModels.EditModel model) {
			var def = Utils.GetDefaultCulture();
			
			//
			// Check that we have a culture other than the default culture
			//
			if (def.Name != CultureInfo.CurrentUICulture.Name) {
				var old = Piranha.Models.Manager.PageModels.EditModel.GetById(model.Page.Id);
				var js = new JavaScriptSerializer();

				using (var db = new Db()) {
					var translation = db.PageTranslations
						.Include(p => p.Regions)
						.Where(p => p.PageId == model.Page.Id)
						.SingleOrDefault();

					if (translation == null) {
						translation = new Entities.PageTranslation() {
							Id = Guid.NewGuid(),
							PageId = model.Page.Id,
							IsDraft = model.Page.IsDraft,
							Culture = CultureInfo.CurrentUICulture.Name
						};
						db.PageTranslations.Add(translation);
					}

					// Map page values
					translation.Title = model.Page.Title;
					translation.NavigationTitle = model.Page.NavigationTitle;
					translation.Keywords = model.Page.Keywords;
					translation.Description = model.Page.Description;

					// Delete old region translations for simplicity
					while (translation.Regions.Count > 0)
						db.RegionTranslations.Remove(translation.Regions[0]);

					// Map regions
					for (var n = 0; n < model.Regions.Count; n++) {
						var region = model.Regions[n];

						var reg = new Entities.RegionTranslation() {
							Id = Guid.NewGuid(),
							PageId = translation.Id,
							RegionId = region.Id,
							TemplateId = region.RegiontemplateId,
							IsDraft = region.IsDraft,
							Type = region.Body.GetType().FullName,
							Culture = CultureInfo.CurrentUICulture.Name
						};
						translation.Regions.Add(reg);

						if (region.Body is IHtmlString)
							reg.Body = ((IHtmlString)region.Body).ToHtmlString();
						else reg.Body = js.Serialize(region.Body);

						// Restore original values
						if (!model.Page.IsNew) {
							model.Page.Title = old.Page.Title;
							model.Page.NavigationTitle = old.Page.NavigationTitle;
							model.Page.Keywords = old.Page.Keywords;
							model.Page.Description = old.Page.Description;
						}

						// Restore original regions
						if (!model.Page.IsNew) {
							region.Body = old.Regions[n].Body;
						}
					}
					db.SaveChanges();
				}
			}
		}
	}
}

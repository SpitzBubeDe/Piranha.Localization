/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace Piranha.Localization
{
	/// <summary>
	/// The main localization module class
	/// </summary>
	public sealed class Module : IHttpModule
	{
		#region Members
		/// <summary>
		/// The internal list of registered languages.
		/// </summary>
		internal static readonly IList<Language> Languages = new List<Language>();
		#endregion

		/// <summary>
		/// Disposes all allocated resources.
		/// </summary>
		public void Dispose() {
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Executed for all requests in the application
		/// </summary>
		/// <param name="context">The current application context</param>
		public void Init(HttpApplication context) {
			context.BeginRequest += (sender, e) => {
				BeginRequest(((System.Web.HttpApplication)sender).Context);
			};
		}

		/// <summary>
		/// Registers a new language. This should be called from the
		/// applications startup code, for example Global.asax.
		/// </summary>
		/// <param name="lang">The language</param>
		public static void RegisterLanguage(Language lang) {
			Piranha.WebPages.WebPiranha.RegisterCulture(lang.UrlPrefix, new CultureInfo(lang.Culture));
			Languages.Add(lang);
		}

		/// <summary>
		/// Executed on each request.
		/// </summary>
		/// <param name="context">The current http context</param>
		private void BeginRequest(HttpContext context) {
			//
			// Get the currently executed route
			//
			string path = context.Request.Path.Substring(context.Request.ApplicationPath.Length > 1 ?
					context.Request.ApplicationPath.Length : 0);
			string[] args = path.Split(new char[] { '/' }).Subset(1);

			//
			// Check if we have a modified culture and we're accessing the manager interface.
			//
			if (Utils.GetDefaultCulture().Name != CultureInfo.CurrentUICulture.Name) {
				if (args.Length > 1 && args[1].ToLower() == "manager") {
					var url = args.Subset(1).Implode("/");

					HttpContext.Current.RewritePath("~/" + args.Subset(1).Implode("/"));
				}
			}
		}
	}
}

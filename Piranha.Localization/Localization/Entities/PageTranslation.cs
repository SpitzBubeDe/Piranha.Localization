/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;

namespace Piranha.Localization.Entities
{
	/// <summary>
	/// Translation for a page.
	/// </summary>
	public sealed class PageTranslation
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets if this is a draft or not.
		/// </summary>
		public bool IsDraft { get; set; }

		/// <summary>
		/// Gets/sets the page id.
		/// </summary>
		public Guid PageId { get; set; }

		/// <summary>
		/// Gets/sets the culture of the translations.
		/// </summary>
		public string Culture { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the naviagtion title.
		/// </summary>
		public string NavigationTitle { get; set; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		public string Description { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the available regions.
		/// </summary>
		public IList<RegionTranslation> Regions { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageTranslation() {
			Regions = new List<RegionTranslation>();
		}
	}
}

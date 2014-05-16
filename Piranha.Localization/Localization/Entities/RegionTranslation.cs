/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Piranha.Localization.Entities
{
	/// <summary>
	/// Translation for a region.
	/// </summary>
	public sealed class RegionTranslation
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
		/// Gets/sets the page translation id.
		/// </summary>
		public Guid PageId { get; set; }

		/// <summary>
		/// Gets/sets the id of the translated id.
		/// </summary>
		public Guid RegionId { get; set; }

		/// <summary>
		/// Gets/sets the id of the region template.
		/// </summary>
		public Guid TemplateId { get; set; }

		/// <summary>
		/// Gets/sets the culture of the translation.
		/// </summary>
		public string Culture { get; set; }

		/// <summary>
		/// Gets/sets the type of region body.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets/sets the JSON serialized body.
		/// </summary>
		public string Body { get; set; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the main page translation.
		/// </summary>
		public PageTranslation Page { get; set; }
		#endregion
	}
}

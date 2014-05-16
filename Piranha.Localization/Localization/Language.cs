/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Piranha.Localization
{
	/// <summary>
	/// A language in the localization module.
	/// </summary>
	public class Language
	{
		/// <summary>
		/// Gets/sets the culture name.
		/// </summary>
		public string Culture { get; set; }

		/// <summary>
		/// Gets/sets the display name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the url prefix
		/// </summary>
		public string UrlPrefix { get; set; }
	}
}

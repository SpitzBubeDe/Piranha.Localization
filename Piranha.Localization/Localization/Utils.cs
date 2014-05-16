/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Globalization;
using System.Web.Configuration;

namespace Piranha.Localization
{
	/// <summary>
	/// Internal utility methods.
	/// </summary>
	internal static class Utils
	{
		/// <summary>
		/// Gets the configured default culture.
		/// </summary>
		/// <returns>The culture</returns>
		public static CultureInfo GetDefaultCulture() {
			return new CultureInfo(((GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization")).UICulture);
		}
	}
}

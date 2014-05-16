/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Piranha.Localization.Entities.Maps
{
	internal class RegionTranslationMap : EntityTypeConfiguration<RegionTranslation>
	{
		public RegionTranslationMap() {
			Property(r => r.Culture).IsRequired().HasMaxLength(8);
		}
	}
}

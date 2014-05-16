/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Piranha.Localization.Entities.Maps
{
	internal class PageTranslationMap : EntityTypeConfiguration<PageTranslation>
	{
		public PageTranslationMap() {
			Property(r => r.Culture).IsRequired().HasMaxLength(8);
			Property(p => p.Title).IsRequired().HasMaxLength(128);
			Property(p => p.NavigationTitle).HasMaxLength(128);
			Property(p => p.Keywords).HasMaxLength(128);
			Property(p => p.Description).HasMaxLength(255);
		}
	}
}

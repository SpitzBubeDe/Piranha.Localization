/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;

namespace Piranha.Localization
{
	/// <summary>
	/// The main translatio db context.
	/// </summary>
	public sealed class Db : DbContext
	{
		#region Db sets
		public DbSet<Entities.PageTranslation> PageTranslations { get; set; }
		public DbSet<Entities.RegionTranslation> RegionTranslations { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Db() : base("piranha") { }

		/// <summary>
		/// Configures the model.
		/// </summary>
		/// <param name="modelBuilder">The modelbuilder</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new Entities.Maps.PageTranslationMap());
			modelBuilder.Configurations.Add(new Entities.Maps.RegionTranslationMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using Zoo.Database.Domain;

namespace Zoo.Database
{
	public class ZooContext : DbContext
	{
		public DbSet<Animal> Animals { get; set; }
		public DbSet<Specie> Species { get; set; }

		public ZooContext() : base("ZooContext") { }
		public ZooContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}
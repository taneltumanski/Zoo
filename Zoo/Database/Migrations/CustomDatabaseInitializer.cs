using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zoo.Database.Migrations
{
	public class CustomDatabaseInitializer : MigrateDatabaseToLatestVersion<ZooContext, Configuration>
	{
		public override void InitializeDatabase(ZooContext context)
		{
			context.Database.CreateIfNotExists();

			base.InitializeDatabase(context);
		}
	}
}
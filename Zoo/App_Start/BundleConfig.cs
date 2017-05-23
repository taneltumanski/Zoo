using System.Web;
using System.Web.Optimization;

namespace Zoo
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/bootstrap-datepicker.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
					  "~/Scripts/angular.js"));

			bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
					  "~/Scripts/jquery.signalR-{version}.js",
					  "~/signalr/hubs"));

			bundles.Add(new ScriptBundle("~/bundles/site").Include(
					  "~/Scripts/site/site.js",
					  "~/Scripts/site/angular/controllers/*.js",
					  "~/Scripts/site/angular/services/*.js",
					  "~/Scripts/site/angular/modules/*.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/bootstrap-theme.css",
					  "~/Content/site.css"));
		}
	}
}

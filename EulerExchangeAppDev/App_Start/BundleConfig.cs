using System.Web;
using System.Web.Optimization;

namespace EulerExchangeAppDev
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //          "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"//,"~/Scripts/bootstrap-datetimepicker-0.0.11/js/bootstrap-datetimepicker.min"
                      ));
            
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
              "~/Scripts/kendo/kendo.all.min.js",
                // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
              "~/Scripts/kendo/kendo.aspnetmvc.min.js"));






            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
               "~/Content/kendo/kendo.common-bootstrap.min.css",
               "~/Content/kendo/kendo.bootstrap.min.css"));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace StudentAccountingSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/csslayout").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Admin/font-awesome/css/font-awesome.min.css",
                      "~/Content/Admin/ioicons/css/ioicons.min.css",
                      "~/Content/Admin/AdminLTE.min.css",
                      "~/Content/Admin/skins/skin-blue.min.css",
                      "~/Content/Admin/jsgrid.min.css",
                      "~/Content/Admin/jsgrid-theme.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/jslayout").Include(
                "~/Scripts/Admin/adminlte.min.js"
                ));
        }
    }
}

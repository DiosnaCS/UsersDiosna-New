using System.Web;
using System.Web.Optimization;

namespace UsersDiosna
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/notification").Include(
                      "~/Scripts/notification.js",
                      "~/Scripts/notifyMe.js",
                      "~/Scripts/ChromeNotification.js"));
            bundles.Add(new ScriptBundle("~/bundles/helpers").Include(
                      "~/Scripts/filter.js",
                      "~/Scripts/notZeroRender.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/hideShow").Include(
                      "~/Scripts/hideShow.js"
                ));
            bundles.Add(new StyleBundle("~/Content/summernote").Include(
                "~/Content/summernote/summernote.css"
            ));
            bundles.Add(new ScriptBundle("~/bundle/summernote").Include(
               "~/Content/summernote/summernote.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                "~/Scripts/chart.js"
            ));
        }
    }
}

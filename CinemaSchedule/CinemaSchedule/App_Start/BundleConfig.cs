using System.Web.Optimization;

namespace CinemaSchedule
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            AddStyles(bundles);
            AddScripts(bundles);

            BundleTable.EnableOptimizations = false;
        }
        private static void AddStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Styles/angular-csp.css",
                "~/Content/Styles/site.css"));
        }
        private static void AddScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/scripts").Include(
                "~/Content/Scripts/jquery.blockUI.js",
                "~/Content/Scripts/Spinner/spin.min.js",
                "~/Content/Scripts/Spinner/jquery.spin.js",
                "~/Content/Scripts/Apps/Filters/unique.js",
                "~/Content/Scripts/common.js"
                ));
        }
    }
}
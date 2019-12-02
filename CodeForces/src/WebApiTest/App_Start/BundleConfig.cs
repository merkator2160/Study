using System.Web.Optimization;

namespace WebApiTest
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-full.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Styles/site.css"));
        }
    }
}

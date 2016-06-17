using System.Web.Optimization;

namespace LunarSFX
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").IncludeDirectory("~/Scripts", "*.js", true));
            bundles.Add(new StyleBundle("~/Content/clean/css").IncludeDirectory("~/Content/themes/clean", "*.css"));

            //Admin bundling
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
              "~/Content/themes/base/all.css"));
        }
    }
}
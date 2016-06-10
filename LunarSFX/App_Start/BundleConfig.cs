using System.Web.Optimization;

namespace LunarSFX
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").IncludeDirectory("~/Scripts", "*.js", true));
            bundles.Add(new StyleBundle("~/Content/clean/css").IncludeDirectory("~/Content/themes/clean", "*.css"));

            //New bundling
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include("~/Scripts/js/jquery.js", "~/Scripts/jqueryui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/styles/jqueryui").IncludeDirectory("~/Content/styles/jqueryui", "*.css"));
            bundles.Add(new StyleBundle("~/styles/jqueryui/custom").IncludeDirectory("~/Content/sunny", "*.css"));
        }
    }
}
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

            bundles.Add(new ScriptBundle("~/scripts/jqgrid").IncludeDirectory("~/Scripts/jqgrid", "*.js", true));
            bundles.Add(new StyleBundle("~/styles/jqgrid").IncludeDirectory("~/Content/styles/jqgrid", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/tinymce").IncludeDirectory("~/Scripts/tinymce", "*.js", true));

            bundles.Add(new StyleBundle("~/styles/admin").IncludeDirectory("~/Content/admin", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/admin").Include("~/Scripts/admin.js"));

        }
    }
}
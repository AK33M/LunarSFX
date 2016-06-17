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

            //New bundling
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include("~/Scripts/js/jquery.js", "~/Scripts/jqueryui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui/css").IncludeDirectory("~/Content/styles/jqueryui", "*.css"));
            bundles.Add(new StyleBundle("~/Content/jqueryui/custom/css").IncludeDirectory("~/Content/sunny", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/jqgrid").Include("~/Scripts/jqgrid/jquery.jqGrid.js","~/Scripts/jqgrid/i18n/grid.locale-en.js"));

            bundles.Add(new StyleBundle("~/Content/jqgrid/css").IncludeDirectory("~/Content/styles/jqgrid", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/tinymce").Include("~/Scripts/tinymce/jquery.tinymce.min.js", "~/Scripts/tinymce/tinymce.min.js"));

            bundles.Add(new StyleBundle("~/Content/admin/css").IncludeDirectory("~/Content/admin", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/admin").Include("~/Scripts/admin.js"));

        }
    }
}
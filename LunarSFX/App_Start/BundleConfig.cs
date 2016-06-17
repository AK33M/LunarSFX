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
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Scripts/js/jquery.js", "~/Scripts/jqueryui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui/css").IncludeDirectory("~/Content/styles/jqueryui", "*.css"));
            bundles.Add(new StyleBundle("~/Content/jqueryui/custom/css").IncludeDirectory("~/Content/sunny", "*.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid/js").Include("~/Scripts/jqgrid/jquery.jqGrid.js","~/Scripts/jqgrid/i18n/grid.locale-en.js"));

            bundles.Add(new StyleBundle("~/Content/jqgrid/css").IncludeDirectory("~/Content/styles/jqgrid", "*.css"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce/js").Include("~/Scripts/tinymce/jquery.tinymce.min.js", "~/Scripts/tinymce/tinymce.min.js"));

            bundles.Add(new StyleBundle("~/Content/admin/css").IncludeDirectory("~/Content/admin", "*.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin/js").Include("~/Scripts/admin.js"));

        }
    }
}
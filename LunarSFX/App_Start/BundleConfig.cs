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
            bundles.Add(new ScriptBundle("~/scripts/jquery/js").Include("~/Scripts/js/jquery.js", "~/Scripts/jqueryui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/styles/jqueryui/css").IncludeDirectory("~/Content/styles/jqueryui", "*.css"));

            bundles.Add(new StyleBundle("~/styles/jqueryui/custom/css").IncludeDirectory("~/Content/sunny", "*.css"));

            bundles.Add(new StyleBundle("~/styles/jqueryui/images/css").IncludeDirectory("~/Content/sunny/images", "*.png"));

            bundles.Add(new ScriptBundle("~/scripts/jqgrid/js").Include("~/Scripts/jqgrid/jquery.jqGrid.js","~/Scripts/jqgrid/i18n/grid.locale-en.js"));

            bundles.Add(new StyleBundle("~/styles/jqgrid/css").IncludeDirectory("~/Content/styles/jqgrid", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/tinymce/js").Include("~/Scripts/tinymce/jquery.tinymce.min.js", "~/Scripts/tinymce/tinymce.min.js"));

            bundles.Add(new StyleBundle("~/styles/admin/css").IncludeDirectory("~/Content/admin", "*.css"));

            bundles.Add(new ScriptBundle("~/scripts/admin/js").Include("~/Scripts/admin.js"));

        }
    }
}
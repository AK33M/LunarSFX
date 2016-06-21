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
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //Admin bundling
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jqgrid").Include("~/Scripts/jquery.jqGrid-5.1.1/js/i18n/grid.locale-en.js",
                                                                     "~/Scripts/jquery.jqGrid-5.1.1/js/jquery.jqGrid.min.js"));
            bundles.Add(new StyleBundle("~/Content/ui.jqGrid/css").IncludeDirectory("~/Scripts/jquery.jqGrid-5.1.1/css", "*.css"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include("~/Content/themes/base/all.css"));
            bundles.Add(new ScriptBundle("~/Scripts/tinymce/js").Include("~/Scripts/tinymce/tinymce.js"));
            bundles.Add(new StyleBundle("~/Content/admin/css").Include("~/Content/admin/admin.css"));
            bundles.Add(new ScriptBundle("~/Scripts/admin/js").Include("~/Scripts/admin.js"));
        }
    }
}
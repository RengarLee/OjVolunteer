using System.Web;
using System.Web.Optimization;

namespace OjVolunteer.UIPortal
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region 
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            #endregion

            #region js
            //oj js
            bundles.Add(new ScriptBundle("~/Content/oj/js").Include(
                        "~/Content/oj/js/jquery-1.12.4.js",
                        "~/Scripts/bootstrap.js"
                        ));

            //layui js
            bundles.Add(new ScriptBundle("~/Content/layui/js").Include(
                        "~/Content/layui/layui.all.js",
                        "~/Content/oj/js/admin.js",
"~/Content/oj/js/check.js",
"~/Content/oj/js/notification.js"));

            //bootstrap-table
            bundles.Add(new ScriptBundle("~/Content/bootstrap-table/js").Include(
                        "~/Content/bootstrap-table/bootstrap-table.js",
                        "~/Content/bootstrap-table/bootstrap-table-zh-CN.js"));

            //bootstrap-table-filter
            bundles.Add(new ScriptBundle("~/Content/bootstrap-table/filter/js").Include(
                        "~/Content/bootstrap-table/bootstrap-table-filter-control.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle("~/Scripts/App/Data").Include(
                        "~/Scripts/App/datapattern.js"));

            #endregion

            #region css
            //oj css
            bundles.Add(new StyleBundle("~/Content/oj/css").Include(
                        "~/Content/oj/css/bootstrap.css", "~/Content/oj/css/main.css"
                        ));

            //layui css
            bundles.Add(new StyleBundle("~/Content/layui/css").Include(
                        "~/Content/layui/css/layui.css"));
            //bootstrap css
            bundles.Add(new StyleBundle("~/Content/bootstrap-table/css").Include(
                        "~/Content/bootstrap-table/bootstrap-table.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-table/filter/css").Include(
                        "~/Content/bootstrap-table/bootstrap-table-filter-control.css"));
            #endregion

        }
    }
}

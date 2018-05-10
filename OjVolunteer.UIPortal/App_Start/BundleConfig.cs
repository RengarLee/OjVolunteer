using System.Web;
using System.Web.Optimization;

namespace OjVolunteer.UIPortal
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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

            //oj js
            bundles.Add(new ScriptBundle("~/Content/oj/js").Include(
                        "~/Content/oj/js/jquery-1.12.4.js",
                        "~/Content/oj/js/bootstrap.min.js",
                        "~/Content/oj/js/check.js"));

            //layui js
            bundles.Add(new ScriptBundle("~/Content/layui/js").Include(
                        "~/Content/layui/layui.all.js"));

            //oj css
            bundles.Add(new StyleBundle("~/Content/oj/css").Include(
          "~/Content/oj/css/main.css"));

            //layui css
            bundles.Add(new StyleBundle("~/Content/layui/css").Include(
          "~/Content/layui/css/layui.css"));
        }
    }
}

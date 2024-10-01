using System.Web;
using System.Web.Optimization;

namespace IAAITaiwanPractice240823
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css/IAAI").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/style.css",
                      "~/css/responsive.css",
                      "~/css/font-awesome.min.css",
                      "~/css/animate.css",
                      "~/css/owl.carousel.css",
                      "~/css/owl.theme.css",
                      "~/css/colorbox.css"));
            bundles.Add(new ScriptBundle("~/js/IAAI").Include(
                      "~/js/jquery.js",
                      "~/js/bootstrap.min.js",
                      "~/js/owl.carousel.min.js",
                      "~/js/jquery.counterup.min.js",
                      "~/js/waypoints.min.js",
                      "~/js/jquery.colorbox.js",
                      "~/js/isotope.js",
                      "~/js/ini.isotope.js",
                      //"http://maps.google.com/maps/api/js?sensor=false",
                      "~/js/gmap3.min.js",
                      "~/js/custom.js"
                ));
        }
    }
}
using System.Web;
using System.Web.Optimization;

namespace WindowsOnly
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/shoppingcart").Include(
                "~/Scripts/jquery-{version}.js", 
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.custom.js",
                "~/Scripts/ViewModels/ShoppingCartSummaryViewModel.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // BundleTable.EnableOptimizations = true;
        }
    }
}

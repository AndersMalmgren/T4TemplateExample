using System.Web;
using System.Web.Optimization;

namespace MyApp.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.binding-conventions-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/app", "*.js", true));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrapbootstrap-theme.css"));

        }
    }
}
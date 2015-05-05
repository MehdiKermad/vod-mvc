using System.Web;
using System.Web.Optimization;

namespace VideoOnDemand
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/modernizr-2.6.2.js",
                      "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace Sorteos.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            //Login css
            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                        "~/Content/lib/materialize/css/materialize.css",
                        "~/Content/lib/awesome-notifications/css/style.css",
                        "~/Content/css/login.css"
            ).Include("~/Content/lib/font-awesome-5.13.0/css/all.min.css", new CssRewriteUrlTransform()));


            //Login js
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/Scripts/jquery-3.4.1.js",
                "~/Scripts/jquery/jquery.validator.js",
                "~/Content/lib/sweet-alert/sweetalert2.all.min.js",
                "~/Content/lib/materialize/js/materialize.js",
                "~/Content/lib/awesome-notifications/js/index.var.js",
                "~/Scripts/public.dialogs.js",
                "~/Scripts/login.js"
            ));

            //Main css

            bundles.Add(new StyleBundle("~/Content/css/main").Include(
                        "~/Content/css/animate.css",
                        "~/Content/css/bootstrap.min.css",
                        "~/Content/css/inspinia.css",
                        "~/Content/css/styles.css",
                        "~/Content/lib/awesome-notifications/css/style.css"
                    ).Include("~/Content/lib/font-awesome-5.13.0/css/all.min.css", new CssRewriteUrlTransform()
            ));

            //Main js
            bundles.Add(new ScriptBundle("~/bundles/main-body").Include(
                "~/Scripts/bootstrap/bootstrap.min.js",
                "~/Scripts/bootstrap/popper.min.js",
                "~/Scripts/inspinia/metisMenu.js",
                "~/Scripts/jquery/jquery.slimscroll.min.js",
                "~/Scripts/inspinia/inspinia.js",
                "~/Scripts/inspinia/pace.min.js",
                "~/Scripts/jquery/jquery.timeago.spanish.js",
                "~/Content/lib/awesome-notifications/js/index.var.js",
                "~/Content/lib/sweet-alert/sweetalert2.all.min.js",
                "~/Scripts/public.dialogs.js",
                "~/Scripts/main.body.js"
            ));


            // Datatables css
            bundles.Add(new StyleBundle("~/Content/css/datatables")
                .Include("~/Content/lib/data-tables/css/buttons.bootstrap4.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/lib/data-tables/css/dataTables.bootstrap4.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/lib/data-tables/css/select.bootstrap4.min.css", new CssRewriteUrlTransform()));


            // Datatables js
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Content/lib/data-tables/js/dataTables.min.js",
                "~/Content/lib/data-tables/js/dataTables.bootstrap4.min.js",
                "~/Content/lib/data-tables/js/buttons.bootstrap4.min.js",
                "~/Content/lib/data-tables/js/buttons.print.min.js",
                "~/Content/lib/data-tables/js/buttons.colVis.min.js",
                "~/Scripts/public.datatable.js"
            ));

            // Tiny MCE
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
               "~/Content/lib/tinymce/tinymce-5.2.2.min.js"));

            // ChartJS css
            bundles.Add(new StyleBundle("~/Content/css/chartjs")
                .Include("~/Content/lib/chartjs/css/chart.css", new CssRewriteUrlTransform()));

            // ChartJS js
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                "~/Content/lib/chartjs/js/chart.bundle.js",
                "~/Content/lib/chartjs/js/chart-plugin-labels.js",
                "~/Scripts/public.chart.js"));


            // JQuery css
            bundles.Add(new StyleBundle("~/Content/css/jquery-steps")
                .Include("~/Content/lib/jquery-steps/css/jquery.steps.css",
                            "~/Content/lib/jquery-steps/css/main.css",
                            "~/Content/lib/jquery-steps/css/normalize.css"));


            // JQuery Steps Js
            bundles.Add(new ScriptBundle("~/bundles/jquery-steps").Include(
                "~/Content/lib/jquery-steps/js/jquery.steps.js",
                "~/Content/lib/jquery-steps/js/jquery-1.9.1.min.js"
                ));

            // FlipTimer css
            bundles.Add(new StyleBundle("~/Content/css/fliptimer")
                .Include("~/Content/lib/fliptimer/css/fliptimer.css"));

            // FlipTimer js
            bundles.Add(new ScriptBundle("~/bundles/fliptimer").Include(
                "~/Content/lib/fliptimer/js/fliptimer.js"
                ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
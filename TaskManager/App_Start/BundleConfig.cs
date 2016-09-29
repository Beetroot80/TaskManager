﻿using System.Web;
using System.Web.Optimization;

namespace TaskManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.12.4.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquryUi").Include(
                "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new StyleBundle("~/jqueryUi/css").Include(
                "~/Content/jquery-ui.min.css",
                "~/Content/jquery-ui.structure.min.css",
                "~/Content/jquery-ui.theme.min.css"));
        }
    }
}

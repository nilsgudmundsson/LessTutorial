using dotless.Core;
using dotless.Core.configuration;
using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace LessTutorial
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            var lessBundle = new Bundle("~/Content/less").Include(
                                                        "~/Content/base.less",
                                                        "~/Content/site.less"
                                                        );
            lessBundle.Transforms.Add(new LessTransform());
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);
        }

public class LessTransform : IBundleTransform
{
    public void Process(BundleContext context, BundleResponse response)
    {
        var content = new StringBuilder();
        foreach (var file in response.Files)
        {
            var filePath = context.HttpContext.Server.MapPath(file.IncludedVirtualPath);
            var importPath = Path.GetDirectoryName(filePath);
            Directory.SetCurrentDirectory(importPath);
            content.Append(Less.Parse(File.ReadAllText(filePath)));
        }
        response.Content = content.ToString();
        response.ContentType = "text/css";
    }
}
    }

 }
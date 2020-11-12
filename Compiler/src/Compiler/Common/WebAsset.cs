using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebCompiler.HtmlBuilder;

namespace Compiler.Common
{
    public abstract class WebAsset
    {
        internal static HtmlBuilder _builder = new HtmlBuilder();

        public WebAsset(string inpath, string outpath)
        {
            FilePath = inpath;
            OutputPath = outpath;
        }

        public string FilePath { get; private set; }
        public string OutputPath { get; private set; }

        public static WebAsset FromString(string filepath)
        {
            string fileExtension = Path.GetExtension(filepath);
            string fileName = Path.GetFileName(filepath);
            WebAsset include = fileExtension switch
            {
                ".css" => new WebAssetStyleSheet(filepath, "css/" + fileName),
                ".js" => new WebAssetJavaScript(filepath, "js/" + fileName),
                _ => new WebAssetUnknown()
            };

            return include;
        }

        public abstract string ToHtml();
    }

    public class WebAssetUnknown : WebAsset
    {
        public WebAssetUnknown() :
            base("", "")
        { }

        public override string ToHtml()
        {
            return "";
        }
    }

    public class WebAssetStyleSheet : WebAsset
    {
        public WebAssetStyleSheet(string inpath, string outpath) :
            base(inpath, outpath)
        {

        }

        public override string ToHtml()
        {
            _builder.PushElement("link", true);
            _builder.AddAttribute("rel", "stylesheet");
            _builder.AddAttribute("href", OutputPath);
            string html = _builder.BuildHTML();
            _builder.Clear();
            return html;
        }
    }

    public class WebAssetJavaScript : WebAsset
    {
        public WebAssetJavaScript(string inpath, string outpath) :
            base(inpath, outpath)
        {

        }

        public override string ToHtml()
        {
            _builder.PushElement("script", false);
            _builder.AddAttribute("src", OutputPath);
            _builder.PopElement();
            string html = _builder.BuildHTML();
            _builder.Clear();
            return html;
        }
    }
}

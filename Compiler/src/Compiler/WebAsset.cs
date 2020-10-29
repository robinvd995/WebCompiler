using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebCompiler.HtmlBuilder;

namespace Compiler
{
    public abstract class WebAsset
    {
        internal static HtmlBuilder _builder = new HtmlBuilder();

        public WebAsset(string filepath)
        {
            FilePath = filepath;
        }

        public string FilePath { get; private set; }

        public static WebAsset FromString(string filepath)
        {
            string fileExtension = Path.GetExtension(filepath);
            WebAsset include = fileExtension switch
            {
                ".css" => new WebAssetStyleSheet(filepath),
                ".js" => new WebAssetJavaScript(filepath),
                _ => new WebAssetUnknown()
            };

            return include;
        }

        public abstract string ToHtml();
    }

    public class WebAssetUnknown : WebAsset
    {
        public WebAssetUnknown() :
            base("")
        { }

        public override string ToHtml()
        {
            return "";
        }
    }

    public class WebAssetStyleSheet : WebAsset
    {
        public WebAssetStyleSheet(string filepath) :
            base(filepath)
        {

        }

        public override string ToHtml()
        {
            _builder.PushElement("link", true);
            _builder.AddAttribute("rel", "stylesheet");
            _builder.AddAttribute("href", FilePath);
            string html = _builder.BuildHTML();
            _builder.Clear();
            return html;
        }
    }

    public class WebAssetJavaScript : WebAsset
    {
        public WebAssetJavaScript(string filepath) :
            base(filepath)
        {

        }

        public override string ToHtml()
        {
            _builder.PushElement("script", true);
            _builder.AddAttribute("src", FilePath);
            string html = _builder.BuildHTML();
            _builder.Clear();
            return html;
        }
    }
}

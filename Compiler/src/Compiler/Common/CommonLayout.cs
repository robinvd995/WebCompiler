using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Common
{
    public class CommonLayout
    {
        private List<WebAsset> _includes = new List<WebAsset>();

        private string _source;

        private int _renderBodyStart = -1;
        private int _renderBodyEnd = -1;

        private int _includeStart = -1;
        private int _includeEnd = -1;

        public static CommonLayout FromConfig(CompilerConfig config)
        {
            string source = File.ReadAllText(config.CommonLayout);

            CommonLayout commonLayout = new CommonLayout();
            commonLayout.SetSource(source);

            foreach (string s in config.Includes)
            {
                WebAsset asset = WebAsset.FromString(s);
                commonLayout.AddInclude(asset);
            }

            return commonLayout;
        }

        public void SetSource(string source)
        {
            int macroStart = -1;

            for(int i = 0; i < source.Length - 1; i++)
            {
                char curChar = source[i];
                char nextChar = source[i + 1];

                if(curChar == '@' && nextChar == '{' && macroStart == -1)
                {
                    macroStart = i;
                }

                if(macroStart > -1 && curChar == '}')
                {
                    string macro = source.Substring(macroStart + 2, i - macroStart - 2);
                    ProcessMacro(macro.Trim(), macroStart, i);
                    macroStart = -1;
                }
            }

            _source = source;
        }

        private void ProcessMacro(string macro, int start, int end)
        {
            switch (macro)
            {
                case "RenderBody":
                    _renderBodyStart = start;
                    _renderBodyEnd = end;
                    break;

                case "Includes":
                    _includeStart = start;
                    _includeEnd = end;
                    break;

                default: throw new CommonLayoutCompileException(string.Format("Unknown Macro Identifier found: {0}", macro));
            }
        }

        public void AddInclude(WebAsset asset)
        {
            _includes.Add(asset);
        }

        public string GetFirstPart()
        {
            string content = _source.Substring(0, _includeStart);
            return content;
        }

        public string GetSecondPart()
        {
            string content = _source.Substring(_includeEnd + 1, _renderBodyStart - _includeEnd - 3);
            return content;
        }

        public string GetThirdPart()
        {
            string content = _source.Substring(_renderBodyEnd + 1);
            return content;
        }

        public string GetIncludes()
        {
            StringBuilder builder = new StringBuilder();
            foreach(WebAsset asset in _includes)
            {
                builder.Append(asset.ToHtml() + '\n');
            }
            return builder.ToString();
        }
    }

    public class CommonLayoutCompileException : Exception
    {
        public CommonLayoutCompileException(string message)
            : base(string.Format("An error occured while compiling CommonLayout: \n {0}", message))
        {

        }
    }
}

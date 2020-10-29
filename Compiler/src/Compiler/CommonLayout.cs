using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class CommonLayout
    {
        private List<WebAsset> _includes = new List<WebAsset>();

        private FragmentFile _source;

        public static CommonLayout FromConfig(CompilerConfig config)
        {
            FragmentFile cfile = FragmentCompiler.CompileFile(config.CommonLayout);

            CommonLayout commonLayout = new CommonLayout();
            commonLayout._source = cfile;
            return commonLayout;
        }

        public void AddInclude(WebAsset asset)
        {
            _includes.Add(asset);
        }
    }
}

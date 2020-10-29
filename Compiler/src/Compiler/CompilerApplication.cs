using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Compiler
{
    public class CompilerApplication
    {
        private CompilerConfig _config;

        private CommonLayout _commonLayout;

        public void LoadConfig(string filepath)
        {
            string configContent = File.ReadAllText(filepath);
            _config = JsonConvert.DeserializeObject<CompilerConfig>(configContent);
        }

        public void Initialize()
        {
            // Initializes CommonLayout
            _commonLayout = CommonLayout.FromConfig(_config);

            // Figure out includes
            foreach(string s in _config.Includes)
            {
                WebAsset asset = WebAsset.FromString(s);
                _commonLayout.AddInclude(asset);
            }
        }

        public void Compile()
        {

        }

        public void FinalizeCompilation()
        {

        }
    }
}

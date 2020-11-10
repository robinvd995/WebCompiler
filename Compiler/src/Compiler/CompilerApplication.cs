using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Compiler.Common;

namespace Compiler
{
    public class CompilerApplication
    {
        private CompilerConfig _config;

        private CommonLayout _commonLayout;

        private List<HtmlPageFile> _pageFiles = new List<HtmlPageFile>();

        public void LoadConfig(string filepath)
        {
            string configContent = File.ReadAllText(filepath);
            _config = JsonConvert.DeserializeObject<CompilerConfig>(configContent);
        }

        public void Initialize()
        {
            // Initializes CommonLayout
            _commonLayout = CommonLayout.FromConfig(_config);

            // Loads all page files from the source folders
            string[] files = Directory.GetFiles(_config.SourceFolder, "*.html");
            int folderLength = _config.SourceFolder.Length;
            foreach(string fileUrl in files)
            {
                string fileName = fileUrl.Substring(folderLength, fileUrl.Length - folderLength);
                HtmlPageFile file = new HtmlPageFile()
                {
                    FileName = fileName,
                    URL = fileUrl
                };
                _pageFiles.Add(file);
            }
        }

        public void Compile()
        {
            foreach(HtmlPageFile htmlFile in _pageFiles)
            {
                FragmentedFile file = FragmentCompiler.CompileFile(htmlFile.URL);
                string fileContent = file.ToString();

                StringBuilder builder = new StringBuilder();
                builder.Append(_commonLayout.GetFirstPart());
                builder.Append(_commonLayout.GetIncludes());
                builder.Append(_commonLayout.GetSecondPart());
                builder.Append(fileContent);
                builder.Append(_commonLayout.GetThirdPart());
                //Console.WriteLine(builder.ToString());

                // Write file to ouput folder!
            }
        }

        public void FinalizeCompilation()
        {
            // Copy the include files to the correct folder!
        }
    }
}

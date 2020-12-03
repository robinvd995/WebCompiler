using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Compiler.Common;
using WebCompiler.Util;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            Logger.Write("Loaded config!");
        }

        public void Initialize()
        {
            // Initializes CommonLayout
            Logger.Write("Parsing common layout...");
            _commonLayout = CommonLayout.FromConfig(_config);

            // Loads all page files from the source folders
            Logger.Write("Scanning folder for files to compile...");
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
            Logger.Write(string.Format("Found {0} files to compile!", _pageFiles.Count));

            if (!Directory.Exists(_config.OutputFolder))
            {
                Logger.Write("Creating output directory!");
                Directory.CreateDirectory(_config.OutputFolder);
            }
        }

        public void Compile()
        {
            foreach(HtmlPageFile htmlFile in _pageFiles)
            {
                Logger.Write(string.Format("Compiling {0}...", htmlFile.FileName));
                FragmentedFile file = FragmentCompiler.CompileFile(htmlFile.URL);
                string fileContent = file.ToString();

                StringBuilder builder = new StringBuilder();
                builder.Append(_commonLayout.GetFirstPart());
                builder.Append(_commonLayout.GetIncludes());
                builder.Append(_commonLayout.GetSecondPart());
                builder.Append(fileContent);
                builder.Append(_commonLayout.GetThirdPart());

                using StreamWriter writer = File.CreateText(_config.OutputFolder + htmlFile.FileName);
                writer.WriteLine(builder);
                writer.Flush();
            }
        }

        public void FinalizeCompilation()
        {
            // Copy the include files to the correct folder!
            Logger.Write("Copying includes into their directory!");

            IEnumerator<WebAsset> enumerator = _commonLayout.GetIncludesEnumerator();
            while (enumerator.MoveNext())
            {
                WebAsset asset = enumerator.Current;
                string outPath = _config.OutputFolder + asset.OutputPath;
                (new FileInfo(outPath)).Directory.Create();
                File.Copy(asset.FilePath, outPath, true);
                Logger.Write(string.Format("Copied file from {0}, to {1}!", asset.FilePath, asset.OutputPath));
            }

            Logger.Write("Compilation finished!");
        }

        public void OpenPage(string page)
        {
            string url = _config.OutputFolder + page;

            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

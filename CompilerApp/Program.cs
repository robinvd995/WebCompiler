using Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            CompilerApplication compiler = new CompilerApplication();
            compiler.LoadConfig("config.json");
            compiler.Initialize();
            compiler.Compile();
            compiler.FinalizeCompilation();

            try
            {
                if (args.Length > 0)
                {
                    foreach (string s in args)
                    {
                        if (s.Contains("OpenPage"))
                        {
                            string pageToOpen = s.Split('=')[1];
                            compiler.OpenPage(pageToOpen);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured while processing arguments!");
            }


            Console.WriteLine("Press any key to close the window...");
            Console.Read();
        }
    }
}

using System;

namespace Compiler
{
    class Start
    {
        static void Main(string[] args)
        {
            CompilerApplication compiler = new CompilerApplication();
            compiler.LoadConfig("config.json");
            compiler.Initialize();
            compiler.Compile();
            compiler.FinalizeCompilation();

            Console.WriteLine("Press any key to close the window...");
            Console.Read();
        }

    }
}

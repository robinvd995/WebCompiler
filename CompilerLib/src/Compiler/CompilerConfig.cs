using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class CompilerConfig
    {
        public string CommonLayout { get; set; }
        public string SourceFolder { get; set; }
        public string OutputFolder { get; set; }

        public string[] Includes { get; set; }
        public string[] Exludes { get; set; }
    }
}

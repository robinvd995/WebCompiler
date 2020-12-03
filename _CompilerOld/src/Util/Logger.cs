using System;
using System.Collections.Generic;
using System.Text;

namespace WebCompiler.Util
{
    public static class Logger
    {
        public static void Write(string message)
        {
            string log = string.Format("[{0}]: {1}", DateTime.Now.ToString("HH:mm:ss"), message);
            Console.WriteLine(log);
        }
    }
}

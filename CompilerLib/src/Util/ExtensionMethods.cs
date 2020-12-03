using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public static class StringExtensions
    {
        public static string BuildString(this StringBuilder builder)
        {
            string value = builder.ToString();
            builder.Clear();
            return value;
        }
    }
}

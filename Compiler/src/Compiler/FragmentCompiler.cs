using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compiler
{
    public static class FragmentCompiler
    {
        public static FragmentFile CompileFile(string filepath)
        {
            FragmentFile cfile = new FragmentFile();

            using (StreamReader reader = new StreamReader(filepath))
            {
                int state = 0;
                StringBuilder builder = new StringBuilder();

                while (reader.Peek() >= 0)
                {
                    char curChar = (char)reader.Read();

                    switch (state)
                    {
                        case 0:
                            char nextChar = (char)reader.Peek();
                            if (curChar == '@' && nextChar == '{')
                            {
                                state = 1;
                                string value = builder.BuildString();

                                cfile.Content.Add(new StringCompilerSection(value));
                            }
                            else
                            {
                                builder.Append(curChar);
                            }
                            break;

                        case 1:
                            if (curChar == '{' || curChar == ' ') continue;
                            if (curChar == '}')
                            {
                                state = 0;
                                string value = builder.BuildString();

                                cfile.Content.Add(new RenderBodyCompilerSection());
                            }
                            else
                            {
                                builder.Append(curChar);
                            }
                            break;
                    }
                }

                if (state == 0)
                {
                    string value = builder.BuildString();

                    cfile.Content.Add(new StringCompilerSection(value));
                }
                else
                {
                    throw new Exception("Compiler state was not 0 when the end of the file was reached!");
                }
            }

            return cfile;
        }
    }

    public interface IDocumentFragment
    {
        string CompileToString();
    }

    public class StringCompilerSection : IDocumentFragment
    {
        private string _value;

        public StringCompilerSection(string value)
        {
            _value = value;
        }

        public string CompileToString()
        {
            return _value;
        }
    }

    public class RenderBodyCompilerSection : IDocumentFragment
    {
        public string CompileToString()
        {
            throw new Exception();
        }
    }

    public class FragmentFile
    {
        public List<IDocumentFragment> Content { get; } = new List<IDocumentFragment>();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (IDocumentFragment s in Content)
            {
                builder.Append(s.CompileToString());
            }

            return builder.ToString();
        }
    }
}

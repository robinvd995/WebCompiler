using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WebEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            PageViewModel pvm = PageViewModel.FromJson("References.json");
            string index0 = ViewModelCompiler.Compile(pvm, "index0");
            Console.WriteLine(index0);
           
        }
    }
}

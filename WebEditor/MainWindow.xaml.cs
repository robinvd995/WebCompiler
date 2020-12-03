using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel viewModel = new MainViewModel();
            viewModel.Pages = new List<string>
            {
                "Index.html",
                "About.html"
            };
            DataContext = viewModel;

        }
    }

    public class SectionItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is SectionViewModel)
            {
                SectionViewModel section = item as SectionViewModel;

                switch (section.Type)
                {
                    default: return element.FindResource("SectionTemplate_Unknown") as DataTemplate;
                    case "p": return element.FindResource("SectionTemplate_Paragraph") as DataTemplate;
                    case "h1": return element.FindResource("SectionTemplate_Header1") as DataTemplate;
                    case "h2": return element.FindResource("SectionTemplate_Header2") as DataTemplate;
                    case "h3": return element.FindResource("SectionTemplate_Header3") as DataTemplate;
                    case "h4": return element.FindResource("SectionTemplate_Header4") as DataTemplate;
                }
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompiler.HtmlBuilder;

namespace WebEditor
{
    public static class ViewModelCompiler
    {
        public static string Compile(PageViewModel viewModel, string identifier)
        {
            ReferenceViewModel refvm = GetReferenceView(viewModel, identifier);
            if (refvm == null) return string.Format("ERROR: {0}", identifier);
            if (refvm.Sections == null || refvm.Sections.Count == 0) return "";

            StringBuilder builder = new StringBuilder();
            foreach(SectionViewModel svm in refvm.Sections)
            {
                HtmlBuilder htmlBuilder = new HtmlBuilder();
                htmlBuilder.PushElement(svm.Type);
                htmlBuilder.SetValue(svm.Content);
                htmlBuilder.PopElement();
                builder.Append(htmlBuilder.BuildHTML());
            }

            return builder.ToString();
        }

        private static ReferenceViewModel GetReferenceView(PageViewModel viewModel, string identifier)
        {
            if (identifier == null || identifier.Length == 0) return null;

            foreach(ReferenceViewModel vm in viewModel.References)
            {
                if (vm.Identifier.Equals(identifier))
                {
                    return vm;
                }
            }

            return null;
        }
    }
}

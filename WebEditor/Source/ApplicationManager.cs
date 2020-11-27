using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEditor
{
    public class ApplicationManager
    {
        private static string _prevSelectedPage = "";

        public static void OnPageChanged(MainViewModel model, string selectedPage)
        {
            if (_prevSelectedPage.Equals(selectedPage)) return;

            // string filepath = ??? + selectedpage
            // model.ActiveModel = PageViewModel.From(filepath)

            _prevSelectedPage = selectedPage;
        }
    }
}

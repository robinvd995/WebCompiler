using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEditor
{
    public class ApplicationManager
    {
        private static ApplicationManager _instance;

        public static ApplicationManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ApplicationManager();
                }
                return _instance;
            }
        }

        private string _prevSelectedPage = "";
        private bool _isEdited = false;

        private ApplicationManager() { }

        public void Initialize()
        {
            
        }

        public void OnPageChanged(MainViewModel model, string selectedPage)
        {
            if (_prevSelectedPage.Equals(selectedPage)) return;
            if (_isEdited)
            {
                // Show unsaved changes prompt
            }

            string filepath = "References/" + selectedPage + ".json";
            model.ActiveModel = PageViewModel.FromJson(filepath);

            _prevSelectedPage = selectedPage;
        }

        public void OnContentChanged()
        {
            _isEdited = true;
        }
    }
}

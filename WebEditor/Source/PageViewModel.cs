using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebEditor
{
    public class PageViewModel : ViewModelBase
    {
        private ReferenceViewModel _activeReference;

        public string Name { get; set; }

        public ReferenceViewModel ActiveReference
        {
            get
            {
                return _activeReference;
            }
            set
            {
                _activeReference = value;
                OnPropertyChanged("ActiveReference");
            }
        }

        public List<ReferenceViewModel> References { get; set; }

        public static PageViewModel FromJson(string filepath)
        {
            string content = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<PageViewModel>(content);
        }
    }

    public class ReferenceViewModel
    {
        public string Identifier { get; set; }

        public List<SectionViewModel> Sections { get; set; }
    }

    public class SectionViewModel : ViewModelBase
    {
        private string _content;

        public string Type { get; set; }
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                ApplicationManager.Instance.OnContentChanged();
                OnPropertyChanged("Content");
            }
        }
    }
}

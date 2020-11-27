using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebEditor
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _selectedPage;

        public MainViewModel()
        {
            
        }

        public List<string> Pages { get; set; }
        public string PageSelected
        {
            get
            {
                return _selectedPage;
            }
            set
            {
                _selectedPage = value;
                ApplicationManager.OnPageChanged(this, _selectedPage);
                OnPropertyChanged("PageSelected");
            }
        }
        public PageViewModel ActiveModel { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

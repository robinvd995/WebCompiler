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
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private string _selectedPage;
        private PageViewModel _activePage;

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
                ApplicationManager.Instance.OnPageChanged(this, _selectedPage);
                OnPropertyChanged("PageSelected");
            }
        }
        public PageViewModel ActiveModel
        {
            get
            {
                return _activePage;
            }
            set
            {
                _activePage = value;
                OnPropertyChanged("ActiveModel");
            }
        }
    }
}

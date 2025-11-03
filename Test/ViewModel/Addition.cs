using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test.ViewModel
{
    public class Addition : INotifyPropertyChanged
    {
        public Addition()
        {
            _A = 0;
            //EventHandlerAdd = new RelayCommand(Add);
        }
        public int A { get { return _A; } set { _A = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("A")); } }
        private int _A;
        public void Add(object? a)
        {
            int b;
            if (!Int32.TryParse((string?)a, out b)) return;
            A += b;
        }
        public ICommand EventHandlerAdd { set; get; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

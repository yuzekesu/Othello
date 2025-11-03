using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Test.ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            object obj = parameter == null ? 0 : parameter;
            return _CanExecute((T)obj);
        }
        public void Execute(object? parameter)
        {
            object obj = parameter == null ? 0 : parameter;
            _Execute((T)obj);
        }
        public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute ?? (_ => true);
        }
        private RelayCommand() { _Execute = _ => { MessageBox.Show("Something wrong with creating relay command."); }; _CanExecute = _ => false; }
        private Action<T> _Execute;
        private Func<T, bool> _CanExecute;
    }
}

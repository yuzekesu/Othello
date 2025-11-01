using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test.ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            return _CanExecute(parameter);
        }
        public void Execute(object? parameter)
        {
            _Execute(parameter);
        }
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute ?? (_ => true);
        }
        private RelayCommand() { }
        private Action<object?> _Execute;
        private Func<object?, bool> _CanExecute;
    }
}

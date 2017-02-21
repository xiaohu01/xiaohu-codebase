using System;
using System.Windows.Input;

namespace CarFinder
{
    public class CarCommand : ICommand
    {
        private Action _execute;
        public event EventHandler CanExecuteChanged;

        public CarCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}

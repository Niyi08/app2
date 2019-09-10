using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfCoreDemo.App
{
    public class RelayCommand : ICommand
    {
        private readonly Action action;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            action();
        }
    }
}

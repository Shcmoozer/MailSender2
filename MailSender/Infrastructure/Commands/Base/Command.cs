﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfMailSender.Infrastructure.Commands.Base
{
    public abstract class Command : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        bool ICommand.CanExecute(object p) => CanExecute(p);
        void ICommand.Execute(object p) => Execute(p);
        protected virtual bool CanExecute(object p) => true;
        protected abstract void Execute(object p);
    }
}
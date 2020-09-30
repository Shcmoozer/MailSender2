using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace WpfMailSender.ViewModels.Base
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual void Set<T>(
            ref T field, T value,
            [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(PropertyName);
        }
    }

}

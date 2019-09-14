using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ReactiveUI;

namespace SmartButler.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, INotifyPropertyChanged
    {
        private bool _isBusy = false;
        public event PropertyChangedEventHandler PropertyChanged;


        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                //OnPropertyChanging();
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string callerName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            //OnPropertyChanging(callerName);
            backingField = value;
            OnPropertyChanged(callerName);

        }


        


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}

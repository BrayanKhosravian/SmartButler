using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ReactiveUI;
using SmartButler.Core;
using SmartButler.Services.Registrable;
using Xamarin.Forms;
using PropertyChangingEventArgs = System.ComponentModel.PropertyChangingEventArgs;
using PropertyChangingEventHandler = System.ComponentModel.PropertyChangingEventHandler;

namespace SmartButler.ViewModels
{
    public abstract class BaseViewModel : ReactiveObject, INotifyPropertyChanged, System.ComponentModel.INotifyPropertyChanging
    {
        private bool _isBusy = false;
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                OnPropertyChanging();
                _isBusy = value;
                OnPropertyChanged();
            }
        }

       


        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string callerName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return;

            OnPropertyChanging(callerName);
            backingField = value;
            OnPropertyChanged(callerName);

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        
    }
}

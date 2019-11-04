using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI;
using PropertyChangingEventArgs = System.ComponentModel.PropertyChangingEventArgs;
using PropertyChangingEventHandler = System.ComponentModel.PropertyChangingEventHandler;

namespace SmartButler.Logic.Common
{
    public abstract class BaseViewModel : ReactiveObject, INotifyPropertyChanged, System.ComponentModel.INotifyPropertyChanging
    {
        private bool _isBusy = false;
        public new event PropertyChangedEventHandler PropertyChanged;
        public new event PropertyChangingEventHandler PropertyChanging;

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

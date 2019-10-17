using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SmartButler.Core;
using SmartButler.Services.Registrable;
using Xamarin.Forms;

namespace SmartButler.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
       
        public SettingsPageViewModel(INavigationService navigationService)
        {
           
        }

        public bool AreButtonsEnabled
        {
            get => Settings.EnableCommands;
            set
            {
                Settings.EnableCommands = value;
                OnPropertyChanged();
            }
        }
    }
}

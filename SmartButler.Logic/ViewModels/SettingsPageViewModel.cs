﻿using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;

namespace SmartButler.Logic.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
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

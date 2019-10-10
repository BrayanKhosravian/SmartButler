using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SmartButler.Services.Registrable;
using SmartButler.Views.Registerable;
using Xamarin.Forms;

namespace SmartButler.ViewModels
{
    public class ToolbarControlViewModel : BaseViewModel
    {

        public ToolbarControlViewModel(INavigationService navigationService)
        {
            SettingsCommand = new Command(async _ => await navigationService.PushAsync<SettingsPage>());
        }

        public ICommand SettingsCommand { get; set; }


    }
}

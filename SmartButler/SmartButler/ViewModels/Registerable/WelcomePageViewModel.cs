using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SmartButler.Core;
using SmartButler.Interfaces;
using SmartButler.Services.RegisterAble;
using SmartButler.Services.Registrable;
using SmartButler.Views;
using SmartButler.Views.Registerable;
using Xamarin.Forms;

namespace SmartButler.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {

        public WelcomePageViewModel(INavigationService navigation, IBluetoothService bluetoothService) 
        {

            BluetoothCommand = new Command(async () => await navigation.PushAsync<BluetoothPage>());

            IngredientsCommand = new Command(async () => await navigation.PushAsync<IngredientsPage>());

            DrinksCommand = new Command(async () => await navigation.PushAsync<DrinksPage>());

            MakeDrinkCommand = new Command(async () => await navigation.PushAsync<MakeDrinkPage>(),
                () => bluetoothService.IsConnected() || Settings.EnableCommands);

          
        }

        public ICommand BluetoothCommand { get; }

        public ICommand IngredientsCommand { get; }

        public ICommand DrinksCommand { get; }

        public ICommand MakeDrinkCommand { get; }


        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }
      
        public void Activate()
        {
            ((Command)MakeDrinkCommand).ChangeCanExecute();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SmartButler.Interfaces;
using SmartButler.Services.Registrable;
using SmartButler.Views;
using Xamarin.Forms;

namespace SmartButler.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {

        public WelcomePageViewModel(INavigationService navigation, IBluetoothService bluetoothService)
        {
            BluetoothCommand = new Command(async () => await navigation.PushAsync<BluetoothPage>());

            BottlesCommand = new Command(async () => await navigation.PushAsync<BottlesPage>());

            DrinksCommand = new Command(async () => await navigation.PushAsync<DrinksPage>());

            MakeDrinkCommand = new Command(async () => await navigation.PushAsync<MakeDrinkPage>(), bluetoothService.IsConnected);
        }

        public ICommand BluetoothCommand { get; }

        public ICommand BottlesCommand { get; }

        public ICommand DrinksCommand { get; }

        public ICommand MakeDrinkCommand { get; }

        public void Activate()
        {
            ((Command)MakeDrinkCommand).ChangeCanExecute();
        }

    }
}

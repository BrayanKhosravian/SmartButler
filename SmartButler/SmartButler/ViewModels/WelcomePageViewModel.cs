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

            BottlesCommand = new Command(async () => await navigation.PushAsync<BottlesPage>(), bluetoothService.IsConnected);

            DrinksCommand = new Command(async () => await navigation.PushAsync<DrinksPage>(), bluetoothService.IsConnected);
        }

        public ICommand BluetoothCommand { get; }

        public ICommand BottlesCommand { get; }

        public ICommand DrinksCommand { get; }

        public void Activate()
        {
            ((Command)BottlesCommand).ChangeCanExecute();
            ((Command)DrinksCommand).ChangeCanExecute();
        }

    }
}

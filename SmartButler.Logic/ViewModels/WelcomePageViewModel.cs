using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;

namespace SmartButler.Logic.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {

        public WelcomePageViewModel(INavigationService navigation, IBluetoothService bluetoothService)
        {

	        BluetoothCommand = ReactiveCommand.Create(async () => await navigation.PushAsync<BluetoothPageViewModel>());

            IngredientsCommand = ReactiveCommand.Create(async () => await navigation.PushAsync<IngredientsPageViewModel>());

            DrinksCommand = ReactiveCommand.Create(async () => await navigation.PushAsync<DrinksPageViewModel>());

            MakeDrinkCommand = (ICommand)ReactiveCommand.Create(async () => await navigation.PushAsync<MakeDrinkPageViewModel>(), 
	            Observable.Return<bool>(bluetoothService.IsConnected() || Settings.EnableCommands));

        }

        public ReactiveCommand BluetoothCommand { get; }

        public ReactiveCommand IngredientsCommand { get; }

        public ReactiveCommand DrinksCommand { get; }

        public ICommand MakeDrinkCommand { get; }


        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }
      
        //public void Activate()
        //{
        //    ((ICommand)MakeDrinkCommand).ChangeCanExecute();
        //}

    }
}

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
	    private readonly IBluetoothService _bluetoothService;

	    public WelcomePageViewModel(INavigationService navigation, IBluetoothService bluetoothService)
        {

	        BluetoothCommand = ReactiveCommand.CreateFromTask(async () => await navigation.PushAsync<BluetoothPageViewModel>());

            IngredientsCommand = ReactiveCommand.CreateFromTask(async () => await navigation.PushAsync<IngredientsPageViewModel>());

            DrinksCommand = ReactiveCommand.CreateFromTask(async () => await navigation.PushAsync<DrinksPageViewModel>());
            
            MakeDrinkCommand = new DelegateCommand(async _ => await navigation.PushAsync<MakeDrinkPageViewModel>(),
	            _ => bluetoothService.IsConnected() || Settings.EnableCommands);

        }

        public ReactiveCommand BluetoothCommand { get; }

        public ReactiveCommand IngredientsCommand { get; }

        public ReactiveCommand DrinksCommand { get; }

        public IDelegateCommand MakeDrinkCommand { get; }


        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

		public void Activate()
		{
			MakeDrinkCommand?.RaiseCanExecuteChanged();
		}

	}
}

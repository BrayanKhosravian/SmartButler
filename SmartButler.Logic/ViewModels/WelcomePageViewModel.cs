using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Input;
using Autofac;
using ReactiveUI;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
    public class WelcomePageViewModel : ToolBarPageViewModelBase
    {
	    public WelcomePageViewModel(INavigationService navigation, IBluetoothService bluetoothService)
        {

	        BluetoothCommand = ReactiveCommand.CreateFromTask(async () => 
		        await navigation.PushAsync<BluetoothPageViewModel>());

            IngredientsCommand = ReactiveCommand.CreateFromTask(async () => 
	            await navigation.PushAsync<ShowIngredientsPageViewModel>());

            DrinksCommand = ReactiveCommand.CreateFromTask(async () => 
	            await navigation.PushAsync<DrinksPageViewModel>());

            MakeDrinkCommand = new DelegateCommand(async _ => await navigation.PushAsync<MakeDrinkPageViewModel>(),
	            _ => bluetoothService.IsConnected() || Settings.EnableCommands);

        }

        public ReactiveCommand BluetoothCommand { get; }
        public ReactiveCommand IngredientsCommand { get; }
        public ReactiveCommand DrinksCommand { get; }
        public IDelegateCommand MakeDrinkCommand { get; }

		public void Activate()
		{
			MakeDrinkCommand?.RaiseCanExecuteChanged();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class DrinksPageViewModel : ToolBarPageViewModelBase
    {
        public ReactiveList<DrinkRecipeViewModel> Drinks { get; } = new ReactiveList<DrinkRecipeViewModel>();

        private readonly IDrinkRecipesRepository _drinkRecipeRepository;
        private readonly IBluetoothService _bluetoothService;
        private readonly INavigationService _navigationService;
        private readonly IUserInteraction _userInteraction;

        public DrinksPageViewModel(
	        IDrinkRecipesRepository drinkRecipeRepository, 
	        INavigationService navigationService, 
	        IBluetoothService bluetoothService,
	        IUserInteraction userInteraction) 
        {
            _drinkRecipeRepository = drinkRecipeRepository;
            _bluetoothService = bluetoothService;
            _navigationService = navigationService;
            _userInteraction = userInteraction;

            AddRecipeCommand = ReactiveCommand.CreateFromTask(async _ =>
                await _navigationService.PushAsync<AddDrinkRecipePageViewModel>());
        }
        public ReactiveCommand AddRecipeCommand { get; }

        public async Task Activate()
        {
	        using (Drinks.SuppressChangeNotifications())
	        {
		        Drinks.Clear();

		        var drinkRecipes = await _drinkRecipeRepository.GetAllAsync(); 
		        var result = drinkRecipes.Select(drinkRecipe => new DrinkRecipeViewModel(drinkRecipe)).ToList();

				Drinks.AddRange(result);
	        }
        }

        public async Task DrinkSelectedAsync(DrinkRecipeViewModel drinkRecipeViewModel)
        {
			var selection = new List<string>(){"Edit", "Delete", "Make drink"};

			var result = await _userInteraction.DisplayActionSheetAsync($"{drinkRecipeViewModel.Name} selected!",
	            "Cancel", null, selection.ToArray());

            if (result == null) return;
	        if (result.Equals("Edit"))
            {
	            var parmeter = new TypedParameter(typeof(DrinkRecipeViewModel), drinkRecipeViewModel);
	            await _navigationService.PushAsync<EditDrinkRecipePageViewModel>(parmeter);
            }
            else if (result.Equals("Delete"))
            {
	            await _drinkRecipeRepository.DeleteAsync(drinkRecipeViewModel.DrinkRecipe);
	            Drinks.Remove(drinkRecipeViewModel);
            }
            else if (result.Equals("Make drink"))
	        {
		        if (!_bluetoothService.IsConnected())
		        {
			        await _userInteraction
				        .DisplayAlertAsync("Info", "Cannot send data to the cocktail machine. \n" + 
						"You are not connected with the bluetooth module of the cocktail machine. \n" +
						"Navigate back to the welcomepage and connect with the device!", "Ok");
			        return;
		        }

				await TransmitAsync(drinkRecipeViewModel);

	        }
        }

        public Task TransmitAsync(DrinkRecipeViewModel drink)
        {
	        var data = drink.ToByteArray();
	        if (_bluetoothService.IsConnected())
		        return _bluetoothService.WriteAsync(data, 0, 0);

	        return Task.CompletedTask;
        }

    }
}

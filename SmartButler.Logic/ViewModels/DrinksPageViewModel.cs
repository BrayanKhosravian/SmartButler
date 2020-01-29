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

namespace SmartButler.Logic.ViewModels
{
	public class DrinksPageViewModel : BaseViewModel, IHasToolBarViewModel
    {
        public ReactiveList<DrinkRecipeViewModel> Drinks { get; private set; } = new ReactiveList<DrinkRecipeViewModel>();

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
                await _navigationService.PushAsync<EditDrinkRecipePageViewModel>());
        }
        public ReactiveCommand AddRecipeCommand { get; }

        public async Task Activate()
        {
	        using (Drinks.SuppressChangeNotifications())
	        {
		        Drinks.Clear();

		        List<DrinkRecipe> drinkRecipes = await _drinkRecipeRepository.GetAllAsync(); // list of my models 
		        var result = drinkRecipes.Select(drinkRecipe => new DrinkRecipeViewModel(drinkRecipe)).ToList();

				Drinks.AddRange(result);
	        }
        }

        public void Transmit(DrinkRecipe drink)
		{
			//var data = drink.ToByteArray();
			if (_bluetoothService.IsConnected())
				_bluetoothService.WriteAsync(new byte[]{0xFF}, 0, 0);
		}

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
	        ToolbarControlViewModel = vm;
        }

        public async Task DrinkSelectedAsync(DrinkRecipeViewModel drinkRecipeViewModel)
        {
			var selection = new List<string>(){"Edit"};
            if(!drinkRecipeViewModel.IsDefault)
                selection.Add("Delete");

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
        }
    }
}

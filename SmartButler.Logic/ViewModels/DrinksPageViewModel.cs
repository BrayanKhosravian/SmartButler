using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.ViewModels
{
	public class IngredientViewModel
	{
		public string Name { get; set; }
		public string Milliliter { get; set; }

	}

	public class DrinkRecipeViewModel
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public byte[] ByteImage { get; set; }

		public ObservableCollection<IngredientViewModel> IngrientViewModels { get; set; } = new ObservableCollection<IngredientViewModel>();

	}

    public class DrinksPageViewModel : BaseViewModel
    {
        public ReactiveList<DrinkRecipeViewModel> Drinks { get; private set; } = new ReactiveList<DrinkRecipeViewModel>();

        private readonly IDrinkRecipesRepository _drinkRecipeRepository;
        private readonly IBluetoothService _bluetoothService;

        public DrinksPageViewModel(IDrinkRecipesRepository drinkRecipeRepository, INavigationService navigationService, IBluetoothService bluetoothService) 
        {
            _drinkRecipeRepository = drinkRecipeRepository;
            _bluetoothService = bluetoothService;

        }

        public async Task Activate()
        {
	        using (Drinks.SuppressChangeNotifications())
	        {
		        Drinks.Clear();

		        var result = new List<DrinkRecipeViewModel>();

		        var drinks = await _drinkRecipeRepository.GetAllAsync();

		        foreach (var drink in drinks)
		        {
					var drinkViewModel = new DrinkRecipeViewModel();

					drinkViewModel.Id = drink.Id;
					drinkViewModel.Name = drink.Name;
			        drinkViewModel.ByteImage = drink.ByteImage;

			        var ingredientViewModels = new ObservableCollection<IngredientViewModel>();

			        foreach (var drinkDrinkIngredient in drink.DrinkIngredients)
			        {
				        var ingredientViewModel = new IngredientViewModel();
				        ingredientViewModel.Name = drinkDrinkIngredient.Ingredient.Name;
				        ingredientViewModel.Milliliter = drinkDrinkIngredient.Milliliter.ToString();
						ingredientViewModels.Add(ingredientViewModel);
			        }

			        drinkViewModel.IngrientViewModels = ingredientViewModels;

					result.Add(drinkViewModel);
		        }

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

    }
}

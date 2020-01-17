using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    }
}

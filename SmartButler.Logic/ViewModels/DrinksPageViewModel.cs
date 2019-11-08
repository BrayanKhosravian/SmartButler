using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.ViewModels
{
    public class DrinksPageViewModel : BaseViewModel
    {
        public ReactiveList<DrinkRecipe> Drinks { get; private set; } = new ReactiveList<DrinkRecipe>();

        private readonly IDrinkRecipesRepository _drinkRecipeRepository;

        public DrinksPageViewModel(IDrinkRecipesRepository drinkRecipeRepository, INavigationService navigationService) 
        {
            _drinkRecipeRepository = drinkRecipeRepository;

        }

        public async Task Activate()
        {
	        using (Drinks.SuppressChangeNotifications())
	        {
		        Drinks.Clear();
				Drinks.AddRange(await _drinkRecipeRepository.GetAllAsync());
	        }
        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

    }
}

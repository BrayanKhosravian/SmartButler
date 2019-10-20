using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Models;
using SmartButler.Services.RegisterAble;
using SmartButler.Services.Registrable;

namespace SmartButler.ViewModels
{
    public class DrinksPageViewModel : BaseViewModel
    {
        public ReactiveList<DrinkRecipe> Drinks { get; private set; } = new ReactiveList<DrinkRecipe>();

        private readonly IDrinkRecipeBuilder _drinkRecipeBuilder;

        public DrinksPageViewModel(IDrinkRecipeBuilder drinkRecipeBuilder, INavigationService navigationService) 
        {
            _drinkRecipeBuilder = drinkRecipeBuilder;

        }


        public void Activate()
        {
            if(!Drinks.Any())
                Drinks.AddRange(AddDefaultDrinks());
        }

        private IEnumerable<DrinkRecipe> AddDefaultDrinks()
        {
            var resolvingType = typeof(DrinksPageViewModel);
            yield return _drinkRecipeBuilder.Default("Madras", "Drinks.Madras.jpg", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Screwdriver", "Drinks.Screwdriver.JPG", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Lemon Drop", "Drinks.Lemondrop.JPG", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Whisky Sour", "Drinks.WhiskySour.JPG", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Blizzard", "Drinks.Blizzard.JPG", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Cape Cod", "Drinks.CapeCod.JPG", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Hot Toddy", "Drinks.HotToddy.JPG", resolvingType).Build();
            yield return _drinkRecipeBuilder.Default("Bourbon Squash", "Drinks.BourbonSquash.JPG", resolvingType).Build();

        }

       

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

    }
}

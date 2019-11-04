using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.ViewModels
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
            yield return _drinkRecipeBuilder.Default("Madras", "Drinks.Madras.jpg", resolvingType)
	            .AddIngredient(new Ingredient(160, "Vodka"))
	            .AddIngredient(new Ingredient(90, "Cranberry juice"))
				.AddIngredient(new Ingredient(30, "Orange juice"))
				.Build();

            yield return _drinkRecipeBuilder.Default("Screwdriver", "Drinks.Screwdriver.JPG", resolvingType)
			   .AddIngredient(new Ingredient(160, "Vodka"))
			   .AddIngredient(new Ingredient(120, "Orange juice"))
			   .Build();

            yield return _drinkRecipeBuilder.Default("Lemon Drop", "Drinks.Lemondrop.JPG", resolvingType)
				.AddIngredient(new Ingredient(160, "Vodka"))
				.AddIngredient(new Ingredient(30, "Lemon juice"))
				.Build();

            yield return _drinkRecipeBuilder.Default("Whisky Sour", "Drinks.WhiskySour.JPG", resolvingType)
				.AddIngredient(new Ingredient(160, "Whisky"))
				.AddIngredient(new Ingredient(15, "Orange juice"))
				.AddIngredient(new Ingredient(30, "Lemon juice"))
				.Build();
           
            yield return _drinkRecipeBuilder.Default("Blizzard", "Drinks.Blizzard.JPG", resolvingType)
				.AddIngredient(new Ingredient(60, "Whisky"))
				.AddIngredient(new Ingredient(30, "Cranberry juice"))
				.AddIngredient(new Ingredient(15, "Lemon juice"))
				.Build();
            
            yield return _drinkRecipeBuilder.Default("Cape Cod", "Drinks.CapeCod.JPG", resolvingType)
	            .AddIngredient(new Ingredient(160, "Vodka"))
	            .AddIngredient(new Ingredient(120, "Cranberry"))
				.Build();
            
            yield return _drinkRecipeBuilder.Default("Hot Toddy", "Drinks.HotToddy.JPG", resolvingType)
				.AddIngredient(new Ingredient(160, "Whisky"))
				.AddIngredient(new Ingredient(15, "Lemon juice"))
				.Build();
            
            yield return _drinkRecipeBuilder.Default("Bourbon Squash", "Drinks.BourbonSquash.JPG", resolvingType)
				.AddIngredient(new Ingredient(160, "Whisky"))
				.AddIngredient(new Ingredient(30, "Orange juice"))
				.AddIngredient(new Ingredient(15, "Lemon juice"))
				.Build();

        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

    }
}

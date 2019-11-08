using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.Services
{
	public interface IDrinkRecipeFactory
	{
		IEnumerable<DrinkRecipe> GetDefaultDrinks();
	}

	public class DrinkRecipeFactory : IDrinkRecipeFactory
	{
		private readonly IDrinkRecipeBuilder _drinkRecipeBuilder;
		private readonly IIngredientFactory _ingredientFactory;

		public DrinkRecipeFactory(IDrinkRecipeBuilder drinkRecipeBuilder)
		{
			_drinkRecipeBuilder = drinkRecipeBuilder;
		}

		public DrinkRecipeFactory(IDrinkRecipeBuilder drinkRecipeBuilder, IIngredientFactory ingredientFactory)
		{
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_ingredientFactory = ingredientFactory;
		}

		public IEnumerable<DrinkRecipe> GetDefaultDrinks()
		{
			yield return _drinkRecipeBuilder.Default("Madras", "Drinks.Madras.jpg")
				.AddIngredient(new Ingredient(160, "Vodka"))
				.AddIngredient(new Ingredient(90, "Cranberry juice"))
				.AddIngredient(new Ingredient(30, "Orange juice"))
				.Build();

			yield return _drinkRecipeBuilder.Default("Screwdriver", "Drinks.Screwdriver.JPG")
			   .AddIngredient(new Ingredient(160, "Vodka"))
			   .AddIngredient(new Ingredient(120, "Orange juice"))
			   .Build();

			yield return _drinkRecipeBuilder.Default("Lemon Drop", "Drinks.Lemondrop.JPG")
				.AddIngredient(new Ingredient(160, "Vodka"))
				.AddIngredient(new Ingredient(30, "Lemon juice"))
				.Build();

			yield return _drinkRecipeBuilder.Default("Whisky Sour", "Drinks.WhiskySour.JPG")
				.AddIngredient(new Ingredient(160, "Whisky"))
				.AddIngredient(new Ingredient(15, "Orange juice"))
				.AddIngredient(new Ingredient(30, "Lemon juice"))
				.Build();

			yield return _drinkRecipeBuilder.Default("Blizzard", "Drinks.Blizzard.JPG")
				.AddIngredient(new Ingredient(60, "Whisky"))
				.AddIngredient(new Ingredient(30, "Cranberry juice"))
				.AddIngredient(new Ingredient(15, "Lemon juice"))
				.Build();

			yield return _drinkRecipeBuilder.Default("Cape Cod", "Drinks.CapeCod.JPG")
				.AddIngredient(new Ingredient(160, "Vodka"))
				.AddIngredient(new Ingredient(120, "Cranberry"))
				.Build();

			yield return _drinkRecipeBuilder.Default("Hot Toddy", "Drinks.HotToddy.JPG")
				.AddIngredient(new Ingredient(160, "Whisky"))
				.AddIngredient(new Ingredient(15, "Lemon juice"))
				.Build();

			yield return _drinkRecipeBuilder.Default("Bourbon Squash", "Drinks.BourbonSquash.JPG")
				.AddIngredient(new Ingredient(160, "Whisky"))
				.AddIngredient(new Ingredient(30, "Orange juice"))
				.AddIngredient(new Ingredient(15, "Lemon juice"))
				.Build();

		}

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
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
			yield return _drinkRecipeBuilder.Default(DrinkNames.Madras, Paths.Drinks.Madras)
				.AddIngredient(new Ingredient(160, IngredientNames.Vodka))
				.AddIngredient(new Ingredient(90, IngredientNames.CranberryJuice))
				.AddIngredient(new Ingredient(30, IngredientNames.OrangeJuice))
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.Screwdriver, Paths.Drinks.Screwdriver)
			   .AddIngredient(new Ingredient(160, IngredientNames.Vodka))
			   .AddIngredient(new Ingredient(120, IngredientNames.OrangeJuice))
			   .Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.LemonDrop, Paths.Drinks.Lemondrop)
				.AddIngredient(new Ingredient(160, IngredientNames.Vodka))
				.AddIngredient(new Ingredient(30, IngredientNames.LemonJuice))
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.WhiskySour, Paths.Drinks.WhiskySour)
				.AddIngredient(new Ingredient(160, IngredientNames.Whisky))
				.AddIngredient(new Ingredient(15, IngredientNames.OrangeJuice))
				.AddIngredient(new Ingredient(30, IngredientNames.LemonJuice))
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.Blizzard, Paths.Drinks.Blizzard)
				.AddIngredient(new Ingredient(60, IngredientNames.Whisky))
				.AddIngredient(new Ingredient(30, IngredientNames.CranberryJuice))
				.AddIngredient(new Ingredient(15, IngredientNames.LemonJuice))
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.CapeCod, Paths.Drinks.CapeCod)
				.AddIngredient(new Ingredient(160, IngredientNames.Vodka))
				.AddIngredient(new Ingredient(120, IngredientNames.CranberryJuice))
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.CapeCod, Paths.Drinks.HotToddy)
				.AddIngredient(new Ingredient(160, IngredientNames.Whisky))
				.AddIngredient(new Ingredient(15, IngredientNames.LemonJuice))
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.BourbonSquash, Paths.Drinks.BourbonSquash)
				.AddIngredient(new Ingredient(160, IngredientNames.Whisky))
				.AddIngredient(new Ingredient(30, IngredientNames.OrangeJuice))
				.AddIngredient(new Ingredient(15, IngredientNames.LemonJuice))
				.Build();

		}

	}
}

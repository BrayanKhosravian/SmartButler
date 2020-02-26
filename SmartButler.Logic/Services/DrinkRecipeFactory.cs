using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SmartButler.Logic.ModelTemplates.Drinks;
using SmartButler.Logic.ModelTemplates.Ingredients;
using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.Services
{
	public interface IDrinkRecipeFactory
	{
		IEnumerable<DrinkRecipe> GetDefaultDrinks();
		T Get<T>() where T : DrinkRecipe;
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

		public T Get<T>() where T : DrinkRecipe => Activator.CreateInstance<T>();


		public IEnumerable<DrinkRecipe> GetDefaultDrinks()
		{
			var madras = _drinkRecipeBuilder.Default(DrinkNames.Madras, Paths.Drinks.Madras)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new CranberryJuice()})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new OrangeJuice()})
				.Build();
			yield return madras;

			var screwdriver = _drinkRecipeBuilder.Default(DrinkNames.Screwdriver, Paths.Drinks.Screwdriver)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(120){Ingredient = new OrangeJuice()})
				.Build();
			yield return screwdriver;

			var lemonDrop = _drinkRecipeBuilder.Default(DrinkNames.LemonDrop, Paths.Drinks.Lemondrop)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new LemonJuice()})
				.Build();
			yield return lemonDrop;

			var whiskySour = _drinkRecipeBuilder.Default(DrinkNames.WhiskySour, Paths.Drinks.WhiskySour)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(15) {Ingredient = new OrangeJuice()})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new LemonJuice()})
				.Build();
			yield return whiskySour;

			var blizzard = _drinkRecipeBuilder.Default(DrinkNames.Blizzard, Paths.Drinks.Blizzard)
				.AddIngredient(new DrinkIngredient(60) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(30) {Ingredient = new CranberryJuice()})
				.AddIngredient(new DrinkIngredient(15) {Ingredient = new LemonJuice()})
				.Build();
			yield return blizzard;

			var capeCod = _drinkRecipeBuilder.Default(DrinkNames.CapeCod, Paths.Drinks.CapeCod)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(120) {Ingredient = new CranberryJuice()})
				.Build();
			yield return capeCod;
			
			var hotToddy = _drinkRecipeBuilder.Default(DrinkNames.HotToddy, Paths.Drinks.HotToddy)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(15) {Ingredient = new LemonJuice()})
				.Build();
			yield return hotToddy;

			var bourbonSquash = _drinkRecipeBuilder.Default(DrinkNames.BourbonSquash, Paths.Drinks.BourbonSquash)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(30) {Ingredient = new OrangeJuice()})
				.AddIngredient(new DrinkIngredient(15){Ingredient = new LemonJuice()})
				.Build();
			yield return bourbonSquash;

		}
	}
}

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

			yield return _drinkRecipeBuilder.Default(DrinkNames.Madras, Paths.Drinks.Madras)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new CranberryJuice()})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new OrangeJuice()})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.Madras, Paths.Drinks.Madras)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Ingredient()
				{
					Id = 2, 
					Name = IngredientNames.Vodka, 
					BottleIndex = 2,
					ByteImage = ResourceManager.GetImageAsBytes(Paths.Ingredients.Vodka)
				}})
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Ingredient()
				{
					Id = 4, 
					Name = IngredientNames.CranberryJuice, 
					BottleIndex = 4,
					ByteImage = ResourceManager.GetImageAsBytes(Paths.Ingredients.CranberryJuice)
				}})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new OrangeJuice()
				{
					Id = 3, 
					Name = IngredientNames.CranberryJuice, 
					BottleIndex = 3,
					ByteImage = ResourceManager.GetImageAsBytes(Paths.Ingredients.CranberryJuice)
				}})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.Screwdriver, Paths.Drinks.Screwdriver)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(120){Ingredient = new OrangeJuice()})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.LemonDrop, Paths.Drinks.Lemondrop)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new LemonJuice()})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.WhiskySour, Paths.Drinks.WhiskySour)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(15) {Ingredient = new OrangeJuice()})
				.AddIngredient(new DrinkIngredient(30){Ingredient = new LemonJuice()})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.Blizzard, Paths.Drinks.Blizzard)
				.AddIngredient(new DrinkIngredient(60) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(30) {Ingredient = new CranberryJuice()})
				.AddIngredient(new DrinkIngredient(15) {Ingredient = new LemonJuice()})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.CapeCod, Paths.Drinks.CapeCod)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Vodka()})
				.AddIngredient(new DrinkIngredient(120) {Ingredient = new CranberryJuice()})
				.Build();
			
			yield return _drinkRecipeBuilder.Default(DrinkNames.HotToddy, Paths.Drinks.HotToddy)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(15) {Ingredient = new LemonJuice()})
				.Build();

			yield return _drinkRecipeBuilder.Default(DrinkNames.BourbonSquash, Paths.Drinks.BourbonSquash)
				.AddIngredient(new DrinkIngredient(160) {Ingredient = new Whisky()})
				.AddIngredient(new DrinkIngredient(30) {Ingredient = new OrangeJuice()})
				.AddIngredient(new DrinkIngredient(15){Ingredient = new LemonJuice()})
				.Build();

			//yield return Get<Madras>();
			//yield return Get<Screwdriver>();
			//yield return Get<LemonDrop>();
			//yield return Get<WhiskySour>();
			//yield return Get<Blizzard>();
			//yield return Get<CapeCod>();
			//yield return Get<HotToddy>();
			//yield return Get<BourbonSquash>();

			

		}
	}
}

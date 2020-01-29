using System;
using System.Collections.Generic;
using System.Linq;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;

namespace SmartButler.Logic.Services
{
	public interface IDrinkRecipeBuilder 
	{
		// from Base
		DrinkRecipeBuilder TakeDefault(DrinkRecipe drinkRecipe);
		DrinkRecipeBuilder Default();
		DrinkRecipeBuilder Default(string name, string resourcePath);
		DrinkRecipeBuilder SetName(string name);
		DrinkRecipeBuilder SetByteImage(string partialResource);
		DrinkRecipeBuilder ClearIngredients();

		// from concretion
		DrinkRecipeBuilder SetIngredients(List<DrinkIngredient> ingredients);
		DrinkRecipeBuilder AddIngredients(params DrinkIngredient[] ingredients);
		DrinkRecipeBuilder AddIngredient(DrinkIngredient ingredient);

	}

	public class DrinkRecipeBuilder : BaseLiquidBuilder<DrinkRecipe, DrinkRecipeBuilder>, IDrinkRecipeBuilder
	{
		private DrinkRecipe _drinkRecipe = new DrinkRecipe();

		protected override DrinkRecipeBuilder BuilderInstance => this;

		public DrinkRecipeBuilder TakeDefault(DrinkRecipe drinkRecipe)
		{
			_drinkRecipe = drinkRecipe;
			return base.TakeDefault(drinkRecipe);
		}

		public override DrinkRecipeBuilder Default()
		{
			_drinkRecipe = new DrinkRecipe();
			return base.Default();
		}

		public override DrinkRecipeBuilder Default(string name, string resourcePath)
		{
			_drinkRecipe = new DrinkRecipe();
			return base.Default(name, resourcePath);
		}

		public DrinkRecipeBuilder ClearIngredients()
		{
			_drinkRecipe.DrinkIngredients.Clear();
			return this;
		}

		public DrinkRecipeBuilder SetIngredients(List<DrinkIngredient> ingredients)
		{
			if (ingredients == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredients' is null!");
			if (ingredients.Count <= 0) throw ExceptionFactory.Get<ArgumentException>("'ingredients' is empty!");
			if (ingredients.Any(ingredient => ingredient == null))
				throw ExceptionFactory.Get<ArgumentNullException>("Any ingredient of 'ingredients' ins null!");

			_drinkRecipe.DrinkIngredients = ingredients;
			return this;
		}

		public DrinkRecipeBuilder AddIngredients(params DrinkIngredient[] ingredients)
		{
			if (ingredients == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredients' is null!");
			if (ingredients.Length <= 0) throw ExceptionFactory.Get<ArgumentException>("'ingredients' is empty!");
			if (ingredients.Any(ingredient => ingredient == null))
				throw ExceptionFactory.Get<ArgumentNullException>("Any ingredient of 'ingredients' ins null!");

			foreach (var ingredient in ingredients)
				_drinkRecipe.DrinkIngredients.Add(ingredient);

			return this;
		}

		public DrinkRecipeBuilder AddIngredient(DrinkIngredient ingredient)
		{
			if (ingredient == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredient' is null");

			_drinkRecipe.DrinkIngredients.Add(ingredient);
			return this;
		}

		public override DrinkRecipe Build()
		{
			if (_drinkRecipe == null) throw ExceptionFactory.Get<NullReferenceException>("'_drinkRecipe' is null!");

			_drinkRecipe.Name = Name;
			_drinkRecipe.ByteImage = ByteImage;

			return _drinkRecipe;
		}

		
	}

}

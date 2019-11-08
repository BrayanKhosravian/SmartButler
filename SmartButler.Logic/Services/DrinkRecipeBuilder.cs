using System;
using System.Collections.Generic;
using System.Linq;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;

namespace SmartButler.Logic.Services
{
	public interface IDrinkRecipeBuilder 
	{
		// from Base
		DrinkRecipeBuilder Default();
		DrinkRecipeBuilder Default(string name, string partialResource);
		DrinkRecipeBuilder SetName(string name);
		DrinkRecipeBuilder SetByteImage(string partialResource);

		// from concretion
		DrinkRecipeBuilder SetIngredients(List<Ingredient> ingredients);
		DrinkRecipeBuilder AddIngredients(params Ingredient[] ingredients);
		DrinkRecipeBuilder AddIngredient(Ingredient ingredient);

	}

	public class DrinkRecipeBuilder : BaseLiquidBuilder<DrinkRecipe, DrinkRecipeBuilder>, IDrinkRecipeBuilder
	{
		private DrinkRecipe _drinkRecipe = new DrinkRecipe();

		protected override DrinkRecipeBuilder BuilderInstance => this;

		public override DrinkRecipeBuilder Default()
		{
			_drinkRecipe = new DrinkRecipe();
			return base.Default();
		}

		public override DrinkRecipeBuilder Default(string name, string partialResource)
		{
			_drinkRecipe = new DrinkRecipe();
			return base.Default(name, partialResource);
		}

		public DrinkRecipeBuilder SetIngredients(List<Ingredient> ingredients)
		{
			if (ingredients == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredients' is null!");
			if (ingredients.Count <= 0) throw ExceptionFactory.Get<ArgumentException>("'ingredients' is empty!");
			if (ingredients.Any(ingredient => ingredient == null))
				throw ExceptionFactory.Get<ArgumentNullException>("Any ingredient of 'ingredients' ins null!");

			_drinkRecipe.Ingredients = ingredients;
			return this;
		}

		public DrinkRecipeBuilder AddIngredients(params Ingredient[] ingredients)
		{
			if (ingredients == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredients' is null!");
			if (ingredients.Length <= 0) throw ExceptionFactory.Get<ArgumentException>("'ingredients' is empty!");
			if (ingredients.Any(ingredient => ingredient == null))
				throw ExceptionFactory.Get<ArgumentNullException>("Any ingredient of 'ingredients' ins null!");

			foreach (var ingredient in ingredients)
				_drinkRecipe.Ingredients.Add(ingredient);

			return this;
		}

		public DrinkRecipeBuilder AddIngredient(Ingredient ingredient)
		{
			if (ingredient == null) throw ExceptionFactory.Get<ArgumentNullException>("'ingredient' is null");

			_drinkRecipe.Ingredients.Add(ingredient);
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

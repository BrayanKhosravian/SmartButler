using System;
using System.Collections.Generic;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SmartButler.Logic.ModelTemplates.Ingredients;

namespace SmartButler.Logic.Services
{
	public interface IIngredientFactory
	{
		IEnumerable<Ingredient> GetDefaultIngredients();
		T Get<T>() where T : Ingredient;
	}

	///<inheritdoc cref="IIngredientFactory"/>
	public class IngredientsFactory : IIngredientFactory
	{
		private readonly IIngredientBuilder _ingredientBuilder;

		public IngredientsFactory(IIngredientBuilder ingredientBuilder)
		{
			_ingredientBuilder = ingredientBuilder;
		}



		public IEnumerable<Ingredient> GetDefaultIngredients()
		{
			yield return Get<Whisky>();
			yield return Get<Vodka>();
			yield return Get<OrangeJuice>();
			yield return Get<CranberryJuice>();
			yield return Get<LemonJuice>();
			yield return Get<None>();


			//yield return _ingredientBuilder.Default(IngredientNames.Whisky, Paths.Ingredients.Whisky)
			//	.SetBottleIndex(1).Build();

			//yield return _ingredientBuilder.Default(IngredientNames.Vodka, Paths.Ingredients.Vodka)
			//	.SetBottleIndex(2).Build();

			//yield return _ingredientBuilder.Default(IngredientNames.OrangeJuice, Paths.Ingredients.OrangeJuice)
			//	.SetBottleIndex(3).Build();

			//yield return _ingredientBuilder.Default(IngredientNames.CranberryJuice, Paths.Ingredients.CranberryJuice)
			//	.SetBottleIndex(4).Build();

			//yield return _ingredientBuilder.Default(IngredientNames.LemonJuice, Paths.Ingredients.LemonJuice)
			//	.SetBottleIndex(5).Build();

			//yield return _ingredientBuilder.Default(IngredientNames.None, Paths.Ingredients.None)
			//	.SetBottleIndex(6).Build();
		}

		public T Get<T>() where T : Ingredient => Activator.CreateInstance<T>();
	}

}

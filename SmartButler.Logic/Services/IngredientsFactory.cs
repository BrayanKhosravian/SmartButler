using System.Collections.Generic;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;

namespace SmartButler.Logic.Services
{
	public interface IIngredientFactory
	{
		IEnumerable<Ingredient> GetDefaultIngredients();
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

			yield return _ingredientBuilder.Default(IngredientNames.Whisky, Paths.Ingredients.Whisky)
				.SetBottleIndex(1).Build();

			yield return _ingredientBuilder.Default(IngredientNames.Vodka, Paths.Ingredients.Vodka)
				.SetBottleIndex(2).Build();

			yield return _ingredientBuilder.Default(IngredientNames.OrangeJuice, Paths.Ingredients.OrangeJuice)
				.SetBottleIndex(3).Build();

			yield return _ingredientBuilder.Default(IngredientNames.CranberryJuice, Paths.Ingredients.CranberryJuice)
				.SetBottleIndex(4).Build();

			yield return _ingredientBuilder.Default(IngredientNames.LemonJuice, Paths.Ingredients.LemonJuice)
				.SetBottleIndex(5).Build();

			yield return _ingredientBuilder.Default(IngredientNames.None, Paths.Ingredients.None)
				.SetBottleIndex(6).Build();
		}
	}

}

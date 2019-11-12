using System.Collections.Generic;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;

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

			yield return _ingredientBuilder.Default(IngredientNames.Whisky, "Bottles.Whisky.jpeg")
				.SetBottleIndex(1).Build();

			yield return _ingredientBuilder.Default(IngredientNames.Vodka, "Bottles.Vodka.jpg")
				.SetBottleIndex(2).Build();

			yield return _ingredientBuilder.Default(IngredientNames.OrangeJuice, "Bottles.OrangeJuice.jpg")
				.SetBottleIndex(3).Build();

			yield return _ingredientBuilder.Default(IngredientNames.CranberryJuice, "Bottles.CranberryJuice.jpg")
				.SetBottleIndex(4).Build();

			yield return _ingredientBuilder.Default(IngredientNames.LemonJuice, "Bottles.LemonJuice.jpg")
				.SetBottleIndex(5).Build();

			yield return _ingredientBuilder.Default(IngredientNames.None, "NONE")
				.SetBottleIndex(6).Build();
		}
	}

}

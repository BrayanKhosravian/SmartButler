using System.Collections.Generic;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.Services
{

	public interface IIngredientFactory
	{
		IEnumerable<Ingredient> GetDefaultIngredients();
	}


	///<inheritdoc cref="IIngredientFactory"/>
	public class IngredientFactory : IIngredientFactory
	{
		private readonly IIngredientBuilder _ingredientBuilder;

		public IngredientFactory(IIngredientBuilder ingredientBuilder)
		{
			_ingredientBuilder = ingredientBuilder;
		}

		public IEnumerable<Ingredient> GetDefaultIngredients()
		{

			yield return _ingredientBuilder.Default("Whisky", "Bottles.Whisky.jpeg")
				.SetBottleIndex(1).Build();

			yield return _ingredientBuilder.Default("Vodka", "Bottles.Vodka.jpg")
				.SetBottleIndex(2).Build();

			yield return _ingredientBuilder.Default("Orange-Juice", "Bottles.OrangeJuice.jpg")
				.SetBottleIndex(3).Build();

			yield return _ingredientBuilder.Default("Cranberry-Juice", "Bottles.CranberryJuice.jpg")
				.SetBottleIndex(4).Build();

			yield return _ingredientBuilder.Default("Lemon-Juice", "Bottles.LemonJuice.jpg")
				.SetBottleIndex(5).Build();

			yield return _ingredientBuilder.Default("NONE", "NONE")
				.SetBottleIndex(6).Build();
		}
	}

}

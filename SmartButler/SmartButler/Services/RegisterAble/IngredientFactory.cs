using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartButler.Models;
using SmartButler.Services.RegisterAble;
using SmartButler.ViewModels;
using SmartButler.ViewModels.RegisterAble;
using Xamarin.Forms;

namespace SmartButler.Services.Registrable
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
			var resolvingType = typeof(IngredientsPageViewModel);

			yield return _ingredientBuilder.Default("Whisky", "Bottles.Whisky.jpeg", resolvingType)
				.SetBottleIndex(1).Build();

			yield return _ingredientBuilder.Default("Vodka", "Bottles.Vodka.jpg", resolvingType)
				.SetBottleIndex(2).Build();

			yield return _ingredientBuilder.Default("Orange-Juice", "Bottles.OrangeJuice.jpg", resolvingType)
				.SetBottleIndex(3).Build();

			yield return _ingredientBuilder.Default("Cranberry-Juice", "Bottles.CranberryJuice.jpg", resolvingType)
				.SetBottleIndex(4).Build();

			yield return _ingredientBuilder.Default("Lemon-Juice", "Bottles.LemonJuice.jpg", resolvingType)
				.SetBottleIndex(5).Build();

			yield return _ingredientBuilder.Default("NONE", "NONE", resolvingType)
				.SetBottleIndex(6).Build();
		}
	}

}

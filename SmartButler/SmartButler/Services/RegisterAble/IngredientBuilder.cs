using System;
using SmartButler.Models;

namespace SmartButler.Services.RegisterAble
{

	public interface IIngredientBuilder //: IBaseLiquidBuilder<Ingredient>
	{
		// from Base
		IngredientBuilder Default();
		IngredientBuilder Default(string name, string partialResource, Type resolvingType);
		IngredientBuilder SetName(string name);
		IngredientBuilder SetImageSource(string partialResource, Type resolvingType);

		// from Concretion
		IngredientBuilder SetMilliliter(int milliliter);
		Ingredient Build();

	}


	public class IngredientBuilder : BaseLiquidBuilder<Ingredient, IngredientBuilder>, IIngredientBuilder
	{
		private Ingredient _ingredient = new Ingredient();

		protected override IngredientBuilder BuilderInstance => this;

		public override IngredientBuilder Default()
		{
			_ingredient = new Ingredient();

			return base.Default();
		}

		public override IngredientBuilder Default(string name, string partialResource, Type resolvingType)
		{
			_ingredient = new Ingredient();

			return base.Default(name, partialResource, resolvingType);
		}

		public IngredientBuilder SetMilliliter(int milliliter)
		{
			if (milliliter <= 0) throw ExceptionFactory.Get<ArgumentException>("'milliliter' cant be <= 0!");

			_ingredient.Milliliter = milliliter;
			return this;
		}

		public override Ingredient Build()
		{
			_ingredient.Name = Name;
			_ingredient.ActualImage = ActualImage;
			_ingredient.ByteImage = ByteImage;

			return _ingredient;

		}

		
	}
}

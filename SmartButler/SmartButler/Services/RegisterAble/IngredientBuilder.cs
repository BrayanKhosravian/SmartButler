using System;
using SmartButler.Models;

namespace SmartButler.Services.RegisterAble
{

	public interface IIngredientBuilder : ILiquidBaseBuilder<Ingredient>
	{
		IngredientBuilder SetMilliliter(int milliliter);
	}


	public class IngredientBuilder : LiquidBaseBuilder<Ingredient>, IIngredientBuilder
	{
		private Ingredient _ingredient = new Ingredient();

		public override LiquidBaseBuilder<Ingredient> Default()
		{
			_ingredient = new Ingredient();

			return base.Default();
		}

		public override LiquidBaseBuilder<Ingredient> Default(string name, string partialResource, Type resolvingType)
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

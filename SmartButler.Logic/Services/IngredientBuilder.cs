using System;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;

namespace SmartButler.Logic.Services
{

	public interface IIngredientBuilder //: IBaseLiquidBuilder<Ingredient>
	{
		// from Base
		IngredientBuilder Default();
		IngredientBuilder Default(string name, string partialResource, Type resolvingType);

		IngredientBuilder SetName(string name);
		IngredientBuilder SetByteImage(string partialResource, Type resolvingType);

		// from Concretion
		IngredientBuilder SetMilliliter(int milliliter);
		IngredientBuilder SetBottleIndex(int index);
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

		public IngredientBuilder SetBottleIndex(int index)
		{
			if (index < 1 || index > 6)
				throw ExceptionFactory.Get<ArgumentException>("'index' has to be between 1 and 6!");

			_ingredient.BottleIndex = index;
			return this;
		}

		public override Ingredient Build()
		{
			_ingredient.Name = Name;
			_ingredient.ByteImage = ByteImage;

			return _ingredient;

		}

		
	}
}

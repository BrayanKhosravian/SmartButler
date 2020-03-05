using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Shouldly;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.Tests
{
	[TestFixture]
	public class DrinkRecipeViewModelTests_ToByteArray
	{
		private class IngredientParameter
		{
			public int BottleIndex { get; }
			public int Milliliter { get; }

			public IngredientParameter(int bottleIndex, int milliliter)
			{
				if(bottleIndex <= 0 || bottleIndex > 6) throw new ArgumentException();
				if(milliliter < 0 || milliliter > 65000) throw new ArgumentException();

				BottleIndex = bottleIndex;
				Milliliter = milliliter;
			}
		}

		[Test]
		[TestCase(1,2,3,4,5,6)]
		[TestCase(6,5,4,3,2,1)]
		[TestCase(1,6,2,5,3,4)]
		public void AfterExecuting_ToByteArray_ResultsBottleIndexes_ShouldBeOrderedAscending(params int[] bottleIndexes)
		{
			// arrange
			var drink = CreateDrinkRecipe(bottleIndexes);
			var drinkVm = new DrinkRecipeViewModel(drink);

			// act
			var result = drinkVm.ToByteArray();

			// assert
			var expected = new byte[] {255, 255, 1, 0, 0, 2, 0, 0, 3, 0, 0, 4, 0, 0, 5, 0, 0, 6, 0, 0, 255, 255,0};
			CollectionAssert.AreEqual(expected, result);
		}


		[Test]
		public void AfterExecuting_ToByteArray_NotExceedingUpperMilliliterBoundary_ShouldNotBeSplit()
		{
			// arrange
			var parameters = new IngredientParameter[]
			{
				new IngredientParameter(1,250),
				new IngredientParameter(2,250),
				new IngredientParameter(3,250),
				new IngredientParameter(4,250),
				new IngredientParameter(5,250),
				new IngredientParameter(6,250),

			};
			var drink = CreateDrinkRecipe(parameters);
			var drinkVm = new DrinkRecipeViewModel(drink);

			// act
			var result = drinkVm.ToByteArray();

			// assert
			var expected = new byte[] {255, 255, 1, 0, 250, 2, 0, 250, 3, 0, 250, 4, 0, 250, 5, 0, 250, 6, 0, 250, 255, 255,0};
			CollectionAssert.AreEqual(expected, result);
		}


		[Test]
		public void AfterExecuting_ToByteArray_ExceedingUpperMilliliterBoundary_ShouldBeSplit()
		{
			// arrange
			var parameters = new IngredientParameter[]
			{
				new IngredientParameter(1,500),
				new IngredientParameter(2,500),
				new IngredientParameter(3,500),
				new IngredientParameter(4,500),
				new IngredientParameter(5,500),
				new IngredientParameter(6,500),

			};
			var drink = CreateDrinkRecipe(parameters);
			var drinkVm = new DrinkRecipeViewModel(drink);

			// act
			var result = drinkVm.ToByteArray();

			// assert
			var expected = new byte[] {255, 255, 1, 250, 250, 2, 250, 250, 3, 250, 250, 4, 250, 250, 5, 250, 250, 6, 250, 250, 255, 255,0};
			CollectionAssert.AreEqual(expected, result);
		}

		[Test]
		public void AfterExecuting_ToByteArray_ExceedingUpperMilliliterBoundary_ShouldBeSplit2()
		{
			// arrange
			var parameters = new IngredientParameter[]
			{
				new IngredientParameter(1,350),
				new IngredientParameter(2,350),
				new IngredientParameter(3,350),
				new IngredientParameter(4,350),
				new IngredientParameter(5,350),
				new IngredientParameter(6,350),

			};
			var drink = CreateDrinkRecipe(parameters);
			var drinkVm = new DrinkRecipeViewModel(drink);

			// act
			var result = drinkVm.ToByteArray();

			// assert
			var expected = new byte[] {255, 255, 1, 100, 250, 2, 100, 250, 3, 100, 250, 4, 100, 250, 5, 100, 250, 6, 100, 250, 255, 255,0};
			CollectionAssert.AreEqual(expected, result);
		}

		[Test]
		public void AfterExecuting_ToByteArray_ExceedingUpperMilliliterBoundary_NotSorted_ShouldBeSplit()
		{
			// arrange
			var parameters = new IngredientParameter[]
			{
				new IngredientParameter(1,351),
				new IngredientParameter(6,352),
				new IngredientParameter(2,353),
				new IngredientParameter(5,354),
				new IngredientParameter(3,355),
				new IngredientParameter(4,356),

			};
			var drink = CreateDrinkRecipe(parameters);
			var drinkVm = new DrinkRecipeViewModel(drink);

			// act
			var result = drinkVm.ToByteArray();

			// assert
			var expected = new byte[] {255, 255, 1, 101, 250, 2, 103, 250, 3, 105, 250, 4, 106, 250, 5, 104, 250, 6, 102, 250, 255, 255,0};
			CollectionAssert.AreEqual(expected, result);
		}


		private DrinkRecipe CreateDrinkRecipe(params int[] bottleIndexes)
		{
			if(bottleIndexes == null) throw new ArgumentNullException();
			if(bottleIndexes.Length <= 0 || bottleIndexes.Length > 6) throw new ArgumentException();
			if(bottleIndexes.Any(i => i > 6 || i < 1)) throw new ArgumentException();

			var result = new DrinkRecipe();
			result.Name = "Test";
			var drinkIngredients = bottleIndexes.Select(bottleIndex => new DrinkIngredient(It.IsAny<int>())
				{Ingredient = new Ingredient() {BottleIndex = bottleIndex}})
				.ToList();

			result.DrinkIngredients = drinkIngredients;

			return result;
		}

		private DrinkRecipe CreateDrinkRecipe(params IngredientParameter[] parameters)
		{
			if(parameters == null) throw new ArgumentNullException();
			if(parameters.Length <= 0 || parameters.Length > 6) throw new ArgumentException();

			var result = new DrinkRecipe();
			result.Name = "Test";
			var drinkIngredients = parameters.Select(parameter => new DrinkIngredient(parameter.Milliliter) 
				{Ingredient = new Ingredient() {BottleIndex = parameter.BottleIndex}})
				.ToList();

			result.DrinkIngredients = drinkIngredients;

			return result;
		}
	}
}

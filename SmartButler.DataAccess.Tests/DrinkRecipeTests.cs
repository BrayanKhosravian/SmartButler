using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;

namespace SmartButler.DataAccess.Tests
{
	public class DrinkRecipeTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ToByteArray_BottleIndexIsNotOrdered_ResultShouldBeOrderedAscending()
		{
			// arrange
			var drinkRecipe = new DrinkRecipe();

			var drinkIngredients = new List<DrinkIngredient>()
			{
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 6}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 1}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 5}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 3}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 2}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 4}}
			};
			drinkRecipe.DrinkIngredients = drinkIngredients;

			// act
			//var result = drinkRecipe.ToByteArray();


			// assert
			var shouldBe = new byte[23]
				{0xFF, 0xFF, 1, 0, 50, 2, 0, 50, 3, 0, 50, 4, 0, 50, 5, 0, 50, 6, 0, 50, 0xFF, 0xFF, 0x00};

			//CollectionAssert.AreEqual(shouldBe, result);
		}

		[Test]
		public void ToByteArray_OnlyFiveIngredients_Result_LastThreeBytesShouldBeZero()
		{
			// arrange
			var drinkRecipe = new DrinkRecipe();
			var drinkIngredients = new List<DrinkIngredient>()
			{
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 1}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 2}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 3}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 4}},
				new DrinkIngredient() {Milliliter = 50, Ingredient = new Ingredient() {BottleIndex = 5}},
			};
			drinkRecipe.DrinkIngredients = drinkIngredients;

			// act
			//var result = drinkRecipe.ToByteArray();


			// assert
			var shouldBe = new byte[23]
				{0xFF, 0xFF, 1, 0, 50, 2, 0, 50, 3, 0, 50, 4, 0, 50, 5, 0, 50, 0, 0, 0, 0xFF, 0xFF, 0x00};

			// CollectionAssert.AreEqual(shouldBe, result);
		}

		

	}
}

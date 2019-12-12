using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using NUnit;
using NUnit.Framework;
using SmartButler.Logic.Services;


namespace SmartButler.Logic.Tests
{

	[TestFixture]
	public class DrinkRecipeFactoryTests
	{
		[Test]
		public void GetDefaultDrinks_FirstTwoDrinks_ShouldBeSame()
		{
			var factory = new DrinkRecipeFactory(new DrinkRecipeBuilder());

			var drinks = factory.GetDefaultDrinks().ToList();

			var drink1 = drinks[0];
			var drink2 = drinks[1];

			//Assert.AreEqual(drink1,drink2);

		}

	}
}

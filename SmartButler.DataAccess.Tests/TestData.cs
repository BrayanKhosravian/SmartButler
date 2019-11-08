using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.DataAccess.Models;

namespace SmartButler.DataAccess.Tests
{
	internal static class TestData
	{
		internal static IEnumerable<Ingredient> CreateDefaultIngredients()
		{
			yield return new Ingredient()
			{
				BottleIndex = 1,
				Name = "I1"
			};

			yield return new Ingredient()
			{
				BottleIndex = 2,
				Name = "I2"
			};
			yield return new Ingredient()
			{
				BottleIndex = 3,
				Name = "I3"
			};
			yield return new Ingredient()
			{
				BottleIndex = 4,
				Name = "I4"
			};
			yield return new Ingredient()
			{
				BottleIndex = 5,
				Name = "I5"
			};
			yield return new Ingredient()
			{
				BottleIndex = 6,
				Name = "I6"
			};
		}

	}
}

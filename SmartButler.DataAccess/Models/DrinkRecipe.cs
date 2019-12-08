using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartButler.DataAccess.Models
{
	[Table("Drinks")]
	public class DrinkRecipe : LiquidBase
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();

		//[Ignore]
		//public List<Ingredient> IngredientsForMapping { get; set; } = new List<Ingredient>();

		[Ignore] public bool IsAvailable => DrinkIngredients.All(i => i.Ingredient.BottleIndex != 0);


		public byte[] ToByteArray()
		{
			byte[] result = new byte[23];
			result[0] = 0xFF;
			result[1] = 0xFF;

			var sorted = DrinkIngredients.OrderBy(drinkIngredient => drinkIngredient.Ingredient.BottleIndex).ToList();

			int i;
			var ingredientCount = 0;
			for (i = 2; i < result.Length - 3; i++)
			{
				var drinkIngredient = sorted.ElementAtOrDefault(ingredientCount);
				if (drinkIngredient == null) break;

				var ml = drinkIngredient.Milliliter;
				byte[] mlBytes = BitConverter.GetBytes(ml);
				Array.Reverse(mlBytes);
				if (mlBytes[2] == 0xFF)
					mlBytes[2] =  (byte)(mlBytes[2] - 1);
				if (mlBytes[3] == 0xFF)
					mlBytes[3] = (byte)(mlBytes[2] - 1);

				result[i] = (byte) drinkIngredient.Ingredient.BottleIndex;
				result[++i] = mlBytes[2];
				result[++i] = mlBytes[3];

				ingredientCount++;
			}

			result[result.Length - 3] = 0xFF;
			result[result.Length - 2] = 0xFF;
			result[result.Length - 1] = 0x00;
			
			return result;

		}
	}
}

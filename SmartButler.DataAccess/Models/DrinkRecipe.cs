using System;
using System.Collections.Generic;
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

		[Ignore]
		public List<Ingredient> IngredientsForMapping { get; set; } = new List<Ingredient>();

		public byte[] ToByteArray()
		{
			byte[] result = new byte[23];
			result[0] = 0xFF;
			result[1] = 0xFF;

			int i;
			for (i = 0; i < DrinkIngredients.Count; i++)
			{
				if (i % 2 == 0) // drink index
				{
					result[i] = (byte)DrinkIngredients[i].Ingredient.BottleIndex;
				}
				else // ingredient ml
				{
					int ml = DrinkIngredients[i].Milliliter;
					byte[] mlBytes = BitConverter.GetBytes(ml);
					Array.Reverse(mlBytes);
					if (mlBytes[0] == 0xFF)
						mlBytes[0] =  (byte)(mlBytes[0] - 1);
					if (mlBytes[1] == 0xFF)
						mlBytes[1] = (byte)(mlBytes[1] - 1);

					result[i] = mlBytes[0];
					result[i+1] = mlBytes[1];
				}
			}

			result[i] = 0xFF;
			result[i + 1] = 0xFF;

			return result;

		}
	}
}

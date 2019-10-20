using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartButler.Models
{
	[Table("DrinkRecipeIngredients")]
	public class DrinkRecipeIngredient : LiquidBase
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[ForeignKey(typeof(DrinkRecipe))]
		public int DrinkId { get; set; }

		public int Milliliter { get; set; }
	}
}

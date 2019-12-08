using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartButler.DataAccess.Models
{
	[Table("DrinkIngredients")]
	public class DrinkIngredient
	{
		public DrinkIngredient()
		{
			
		}

		public DrinkIngredient(int milliliter)
		{
			Milliliter = milliliter;
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int Milliliter { get; set; }

		[ForeignKey(typeof(DrinkRecipe))]
		public int DrinkId { get; set; }

		[ForeignKey(typeof(Ingredient))]
		public int IngredientId { get; set; }

		[ManyToOne(CascadeOperations = CascadeOperation.All)]
		public Ingredient Ingredient { get; set; }
	}
}

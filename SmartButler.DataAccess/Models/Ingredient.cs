﻿using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartButler.DataAccess.Models
{
	[Table("Ingredients")]
	public class Ingredient : LiquidBase
    {
	    public Ingredient()
	    {

	    }

	    public Ingredient(int milliliter, string name) : base(name)
	    {
		    Milliliter = milliliter;
	    }

	    [PrimaryKey, AutoIncrement]
	    public int Id { get; set; }

	    [ForeignKey(typeof(DrinkRecipe))]
	    public int DrinkId { get; set; }

	    public int Milliliter { get; set; }

		public int? BottleIndex { get; set; } = null;

		[Ignore] public bool IsAvailable => BottleIndex != null;
	}
}
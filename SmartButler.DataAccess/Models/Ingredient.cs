using System;
using SQLite;
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

	    public Ingredient(int milliliter)
	    {
		    Milliliter = milliliter;
	    }


	    [PrimaryKey, AutoIncrement]
	    public int Id { get; set; }

	    [Obsolete("Bridge table is in use - multistep refactoring")]
	    public int Milliliter { get; set; }

		public int BottleIndex { get; set; }

		[Ignore] public bool IsAvailable => BottleIndex >= 1 && BottleIndex <= 6;
	}
}

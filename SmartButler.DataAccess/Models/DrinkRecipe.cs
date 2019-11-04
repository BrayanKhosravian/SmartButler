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

	    [OneToMany]
		public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();


    }
}

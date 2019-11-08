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


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Xamarin.Forms;

namespace SmartButler.Models
{
	[Table("Drinks")]
	public class DrinkRecipe : LiquidBase
    {
	    [PrimaryKey, AutoIncrement]
	    public int Id { get; set; }

	    [OneToMany]
		public ReactiveList<Ingredient> Ingredients { get; set; } = new ReactiveList<Ingredient>();


    }
}

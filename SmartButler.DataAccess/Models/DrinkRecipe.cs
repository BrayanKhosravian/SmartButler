using System;
using System.Collections.Generic;
using System.Linq;
using SmartButler.DataAccess.Common;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartButler.DataAccess.Models
{
	[Table(TableNames.DrinkRecipeTable)]
	public class DrinkRecipe : LiquidBase
	{
		[PrimaryKey, AutoIncrement]
		public virtual int Id { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public virtual List<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();

	}
}

using System;
using SmartButler.DataAccess.Common;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartButler.DataAccess.Models
{
	[Table(TableNames.IngredientsTable)]
	public class Ingredient : LiquidBase
    {
	    public Ingredient()
	    {

	    }

	    [PrimaryKey, AutoIncrement]
	    public virtual int Id { get; set; }
	    public virtual int BottleIndex { get; set; }
    }
}

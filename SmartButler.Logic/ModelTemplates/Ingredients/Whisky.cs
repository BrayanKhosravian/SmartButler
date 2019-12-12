using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Ingredients
{
	[Table(TableNames.IngredientsTable)]
	public sealed class Whisky : Ingredient
	{
		public override int Id => 1;
		public override string Name => IngredientNames.Whisky;
		public override byte[] ByteImage =>
			SmartButler.Framework.Resources.ResourceManager.GetImageAsBytes(Paths.Ingredients.Whisky);
		public override int BottleIndex => 1;
	}
}

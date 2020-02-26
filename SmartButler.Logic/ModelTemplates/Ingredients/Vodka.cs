using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Ingredients
{
	[Table(TableNames.IngredientsTable)]
	public sealed class Vodka : Ingredient
	{
		public override int Id => 2;
		public override string Name => IngredientNames.Vodka;
		public override byte[] ByteImage =>
			ResourceManager.GetImageAsBytes(Paths.Ingredients.Vodka);
		public override int BottleIndex => 2;
	}
}
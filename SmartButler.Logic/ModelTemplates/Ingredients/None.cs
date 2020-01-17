using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Ingredients
{
	[Table(TableNames.IngredientsTable)]
	public sealed class None : Ingredient
	{
		public override int Id => 6;
		public override string Name => IngredientNames.None;
		public override byte[] ByteImage =>
			ResourceManager.GetImageAsBytes(Paths.Ingredients.None);
		public override int BottleIndex => 6;
		public override bool IsDefault => true;
	}
}
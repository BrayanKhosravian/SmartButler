using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Ingredients
{
	[Table(TableNames.IngredientsTable)]
	public sealed class OrangeJuice : Ingredient
	{
		public override int Id => 3;
		public override string Name => IngredientNames.OrangeJuice;
		public override byte[] ByteImage =>
			ResourceManager.GetImageAsBytes(Paths.Ingredients.OrangeJuice);
		public override int BottleIndex => 3;
		public override bool IsDefault => true;
	}
}
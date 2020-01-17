using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Ingredients
{
	[Table(TableNames.IngredientsTable)]
	public sealed class CranberryJuice : Ingredient
	{
		public override int Id => 4;
		public override string Name => IngredientNames.CranberryJuice;
		public override byte[] ByteImage =>
			ResourceManager.GetImageAsBytes(Paths.Ingredients.CranberryJuice);
		public override int BottleIndex => 4;
		public override bool IsDefault => true;
	}
}
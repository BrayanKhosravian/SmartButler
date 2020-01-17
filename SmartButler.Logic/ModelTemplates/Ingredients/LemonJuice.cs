using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Ingredients
{
	[Table(TableNames.IngredientsTable)]
	public sealed class LemonJuice : Ingredient
	{
		public override int Id => 5;
		public override string Name => IngredientNames.LemonJuice;
		public override byte[] ByteImage => 
			ResourceManager.GetImageAsBytes(Paths.Ingredients.LemonJuice);
		public override int BottleIndex => 5;
		public override bool IsDefault => true;
	}
}
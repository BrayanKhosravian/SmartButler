using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Framework.Resources;
using SmartButler.Logic.ModelTemplates.Ingredients;
using SQLite;

namespace SmartButler.Logic.ModelTemplates.Drinks
{
	[Table(TableNames.DrinkRecipeTable)]
	public sealed class Madras : DrinkRecipe
	{
		public override int Id => 1;
		public override string Name => DrinkNames.Madras;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.Madras);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 1,
				DrinkId = 1,
				Milliliter = 160,
				IngredientId = 2,
				Ingredient = new Vodka()
			},
			new DrinkIngredient()
			{
				Id = 2,
				DrinkId = 1,
				Milliliter = 160,
				IngredientId = 4,
				Ingredient = new CranberryJuice()
			},
			new DrinkIngredient()
			{
				Id = 3,
				DrinkId = 1,
				Milliliter = 30,
				IngredientId = 3,
				Ingredient = new OrangeJuice()
			}
		};
	}

	[Table(TableNames.DrinkRecipeTable)]
	public sealed class Screwdriver : DrinkRecipe
	{
		public override int Id => 2;
		public override string Name => DrinkNames.Screwdriver;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.Screwdriver);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 4,
				DrinkId = 2,
				Milliliter = 160,
				IngredientId = 2,
				Ingredient = new Vodka()
			},
			new DrinkIngredient()
			{
				Id = 5,
				DrinkId = 2,
				Milliliter = 120,
				IngredientId = 3,
				Ingredient = new OrangeJuice()
			}
		};
	}


	[Table(TableNames.DrinkRecipeTable)]
	public sealed class LemonDrop : DrinkRecipe
	{
		public override int Id => 3;
		public override string Name => DrinkNames.LemonDrop;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.Lemondrop);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 6,
				DrinkId = 3,
				Milliliter = 160,
				IngredientId = 2,
				Ingredient = new Vodka()
			},
			new DrinkIngredient()
			{
				Id = 7,
				DrinkId = 3,
				Milliliter = 30,
				IngredientId = 5,
				Ingredient = new LemonJuice()
			}
		};
	}


	[Table(TableNames.DrinkRecipeTable)]
	public sealed class WhiskySour : DrinkRecipe
	{
		public override int Id => 4;
		public override string Name => DrinkNames.WhiskySour;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.WhiskySour);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 8,
				DrinkId = 4,
				Milliliter = 160,
				IngredientId = 1,
				Ingredient = new Whisky()
			},
			new DrinkIngredient()
			{
				Id = 9,
				DrinkId = 4,
				Milliliter = 15,
				IngredientId = 3,
				Ingredient = new OrangeJuice()
			},
			new DrinkIngredient()
			{
				Id = 10,
				DrinkId = 4,
				Milliliter = 30,
				IngredientId = 5,
				Ingredient = new LemonJuice()
			}
		};
	}

	[Table(TableNames.DrinkRecipeTable)]
	public sealed class Blizzard : DrinkRecipe
	{
		public override int Id => 5;
		public override string Name => DrinkNames.Blizzard;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.Blizzard);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 11,
				DrinkId = 5,
				Milliliter = 60,
				IngredientId = 1,
				Ingredient = new Whisky()
			},
			new DrinkIngredient()
			{
				Id = 12,
				DrinkId = 5,
				Milliliter = 30,
				IngredientId = 4,
				Ingredient = new CranberryJuice()
			},
			new DrinkIngredient()
			{
				Id = 13,
				DrinkId = 5,
				Milliliter = 15,
				IngredientId = 5,
				Ingredient = new LemonJuice()
			}
		};
	}


	[Table(TableNames.DrinkRecipeTable)]
	public sealed class CapeCod : DrinkRecipe
	{
		public override int Id => 6;
		public override string Name => DrinkNames.CapeCod;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.CapeCod);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 14,
				DrinkId = 6,
				Milliliter = 160,
				IngredientId = 2,
				Ingredient = new Vodka()
			},
			new DrinkIngredient()
			{
				Id = 15,
				DrinkId = 6,
				Milliliter = 120,
				IngredientId = 4,
				Ingredient = new CranberryJuice()
			}
		};
	}

	[Table(TableNames.DrinkRecipeTable)]
	public sealed class HotToddy : DrinkRecipe
	{
		public override int Id => 7;
		public override string Name => DrinkNames.HotToddy;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.HotToddy);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 16,
				DrinkId = 7,
				Milliliter = 160,
				IngredientId = 1,
				Ingredient = new Whisky()
			},
			new DrinkIngredient()
			{
				Id = 17,
				DrinkId = 7,
				Milliliter = 15,
				IngredientId = 5,
				Ingredient = new LemonJuice()
			}
		};
	}

	[Table(TableNames.DrinkRecipeTable)]
	public sealed class BourbonSquash : DrinkRecipe
	{
		public override int Id => 8;
		public override string Name => DrinkNames.BourbonSquash;
		public override byte[] ByteImage => ResourceManager.GetImageAsBytes(Paths.Drinks.BourbonSquash);
		public override List<DrinkIngredient> DrinkIngredients => new List<DrinkIngredient>()
		{
			new DrinkIngredient()
			{
				Id = 18,
				DrinkId = 8,
				Milliliter = 160,
				IngredientId = 1,
				Ingredient = new Whisky()
			},
			new DrinkIngredient()
			{
				Id = 19,
				DrinkId = 8,
				Milliliter = 30,
				IngredientId = 3,
				Ingredient = new OrangeJuice()
			},
			new DrinkIngredient()
			{
				Id = 20,
				DrinkId = 8,
				Milliliter = 15,
				IngredientId = 5,
				Ingredient = new LemonJuice()
			}
		};
	}

}

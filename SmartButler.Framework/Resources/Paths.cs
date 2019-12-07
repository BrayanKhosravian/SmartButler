using System;
using System.Collections.Generic;
using System.Text;

namespace SmartButler.Framework.Resources
{
	public static class Paths
	{
		private const string BasePath = "SmartButler.Framework.Resources";

		public static class Ingredients
		{
			private static readonly string IngredientPath = $"{BasePath}.Ingredients";

			public static readonly string CranberryJuice = $"{IngredientPath}.CranberryJuice.jpg";
			public static readonly string LemonJuice = $"{IngredientPath}.LemonJuice.jpg";
			public static readonly string None = $"{IngredientPath}.None.jpg";
			public static readonly string OrangeJuice = $"{IngredientPath}.OrangeJuice.jpg";
			public static readonly string Vodka = $"{IngredientPath}.Vodka.jpg";
			public static readonly string Whisky = $"{IngredientPath}.Whisky.jpeg";
		}

		public static class Drinks
		{
			private static readonly string DrinksPath = $"{BasePath}.Drinks";

			public static readonly string Blizzard = $"{DrinksPath}.Blizzard.JPG";
			public static readonly string BourbonSquash = $"{DrinksPath}.BourbonSquash.JPG";
			public static readonly string CapeCod = $"{DrinksPath}.CapeCod.JPG";
			public static readonly string HotToddy = $"{DrinksPath}.HotToddy.JPG";
			public static readonly string Lemondrop = $"{DrinksPath}.Lemondrop.JPG";
			public static readonly string Madras = $"{DrinksPath}.Madras.jpg";
			public static readonly string Screwdriver = $"{DrinksPath}.Screwdriver.JPG";
			public static readonly string WhiskySour = $"{DrinksPath}.WhiskySour.JPG";
		}

	}
}

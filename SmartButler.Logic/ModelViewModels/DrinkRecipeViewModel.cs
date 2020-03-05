using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Logic.Common;

namespace SmartButler.Logic.ModelViewModels
{
	public class DrinkRecipeViewModel : ViewModelBase
	{
		private ObservableCollection<DrinkIngredientViewModel> _ingredientViewModels;

		private string _name;
		private int _id;
		private byte[] _byteImage;

		public DrinkRecipeViewModel(DrinkRecipe drinkRecipe)
		{
			if(drinkRecipe == null) throw ExceptionFactory.Get<ArgumentNullException>("'drinkRecipe' was null'");
			
			if(drinkRecipe.DrinkIngredients == null)
				throw ExceptionFactory.Get<ArgumentNullException>("'drinkRecipe.DrinkIngredients' was null'");

			if(string.IsNullOrWhiteSpace(drinkRecipe.Name)) 
				throw ExceptionFactory.Get<ArgumentNullException>("'drinkRecipe.Name' was null'");

			Id = drinkRecipe.Id;
			Name = drinkRecipe.Name;
			ByteImage = drinkRecipe.ByteImage;

			DrinkRecipe = drinkRecipe;

			MapModelWithViewModel(drinkRecipe);
		}

		public ObservableCollection<DrinkIngredientViewModel> IngredientViewModels => 
			_ingredientViewModels ?? new ObservableCollection<DrinkIngredientViewModel>(MapModelWithViewModel(DrinkRecipe));

		public bool IsAvailable => IngredientViewModels.All(i => i.IsAvailable);

		public string Name
		{
			get => _name;
			set => SetValue(ref _name, value);
		}

		public int Id
		{
			get => _id;
			set => SetValue(ref _id, value);
		}

		public byte[] ByteImage
		{
			get => _byteImage;
			set => SetValue(ref _byteImage, value);
		}

		public DrinkRecipe DrinkRecipe { get; }

		private IEnumerable<DrinkIngredientViewModel> MapModelWithViewModel(DrinkRecipe drinkRecipe)
		{
			var ingredientViewModels = new ObservableCollection<DrinkIngredientViewModel>();

			foreach (var drinkDrinkIngredient in drinkRecipe.DrinkIngredients)
			{
				var ingredientViewModel = new DrinkIngredientViewModel(drinkDrinkIngredient.Ingredient, drinkDrinkIngredient);
				ingredientViewModels.Add(ingredientViewModel);
			}

			return ingredientViewModels;
		}

		public byte[] ToByteArray()
		{
			byte[] result = new byte[23];
			result[0] = 0xFF;
			result[1] = 0xFF;

			var sorted = IngredientViewModels.OrderBy(drinkIngredient => drinkIngredient.BottleIndex).ToList();

			int i;
			var ingredientCount = 0;
			for (i = 2; i < result.Length - 3; i++)
			{
				var drinkIngredient = sorted.ElementAtOrDefault(ingredientCount);
				if (drinkIngredient == null) break;

				var ml = drinkIngredient.Milliliter;
				if(ml > 500) throw new ArgumentException();

				int ml1, ml2;
				if (ml > 250)
				{
					ml1 = 250;
					ml2 = ml - 250;
				}
				else
				{
					ml1 = ml;
					ml2 = 0;
				}

				result[i] = (byte) drinkIngredient.BottleIndex;
				result[++i] = (byte) ml2;
				result[++i] = (byte) ml1;

				ingredientCount++;
			}

			result[result.Length - 3] = 0xFF;
			result[result.Length - 2] = 0xFF;
			result[result.Length - 1] = 0x00;
			
			return result;

		}

	}

}
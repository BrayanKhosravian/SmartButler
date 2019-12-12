using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Common;
using SmartButler.Logic.Common;
using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.ModelViewModels
{
	public class DrinkRecipeViewModel : BaseViewModel
	{
		private ObservableCollection<IngredientViewModel> _ingredientViewModels;

		private readonly DrinkRecipe _drinkRecipe;
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

			_drinkRecipe = drinkRecipe;

			MapModelWithViewModel(drinkRecipe);
		}

		public ObservableCollection<IngredientViewModel> IngredientViewModels => 
			_ingredientViewModels ?? new ObservableCollection<IngredientViewModel>(MapModelWithViewModel(_drinkRecipe));

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

		private IEnumerable<IngredientViewModel> MapModelWithViewModel(DrinkRecipe drinkRecipe)
		{
			var ingredientViewModels = new ObservableCollection<IngredientViewModel>();

			foreach (var drinkDrinkIngredient in drinkRecipe.DrinkIngredients)
			{
				var ingredientViewModel = new IngredientViewModel(drinkDrinkIngredient.Ingredient, drinkDrinkIngredient);
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
				byte[] mlBytes = BitConverter.GetBytes(ml);
				Array.Reverse(mlBytes);
				if (mlBytes[2] == 0xFF)
					mlBytes[2] =  (byte)(mlBytes[2] - 1);
				if (mlBytes[3] == 0xFF)
					mlBytes[3] = (byte)(mlBytes[2] - 1);

				result[i] = (byte) drinkIngredient.BottleIndex;
				result[++i] = mlBytes[2];
				result[++i] = mlBytes[3];

				ingredientCount++;
			}

			result[result.Length - 3] = 0xFF;
			result[result.Length - 2] = 0xFF;
			result[result.Length - 1] = 0x00;
			
			return result;

		}

	}

}
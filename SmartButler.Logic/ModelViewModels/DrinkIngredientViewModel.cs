using System;
using System.Windows.Input;
using Plugin.Media;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.Common;
using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.ModelViewModels
{
	public class DrinkIngredientViewModel : DrinkIngredientBaseViewModel
	{
		private string _name;
		private int _milliliter;
		private int _bottleIndex;
		private byte[] _byteImage;
		private bool _isDefault;

		public DrinkIngredientViewModel(Ingredient ingredient)
		{
			Ingredient = ingredient;

			Name = ingredient.Name;
			BottleIndex = ingredient.BottleIndex;
			ByteImage = ingredient.ByteImage;

			IsDefault = ingredient.IsDefault;
		}

		public DrinkIngredientViewModel(Ingredient ingredient, DrinkIngredient drinkIngredient)
		{
			Ingredient = ingredient;
			DrinkIngredient = drinkIngredient;

			Name = ingredient.Name;
			BottleIndex = ingredient.BottleIndex;
			ByteImage = ingredient.ByteImage;

			Milliliter = drinkIngredient.Milliliter;
			IsDefault = ingredient.IsDefault;
		}

		public Ingredient Ingredient { get; }
		public DrinkIngredient DrinkIngredient { get; }

		public string Name
		{
			get => _name;
			set => SetValue(ref _name, value);
		}

		public int Milliliter
		{
			get => _milliliter;
			set => SetValue(ref _milliliter, value);
		}

		public int BottleIndex
		{
			get => _bottleIndex;
			set => SetValue(ref _bottleIndex, value);
		}

		public byte[] ByteImage
		{
			get => _byteImage;
			set => SetValue(ref _byteImage, value);
		}

		public bool IsAvailable => _bottleIndex != 0;

		public bool IsDefault
		{
			get => _isDefault;
			set => SetValue(ref _isDefault, value);
		}

		public void UpdateIngredientModel()
		{
			Ingredient.Name = Name;
			Ingredient.BottleIndex = BottleIndex;
			Ingredient.ByteImage = ByteImage;
			Ingredient.IsDefault = IsDefault;
		}

		public void UpdateDrinkIngredientModel()
		{
			DrinkIngredient.Milliliter = Milliliter;
		}

	}
}
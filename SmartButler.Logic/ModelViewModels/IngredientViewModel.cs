using System;
using System.Windows.Input;
using Plugin.Media;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.Common;

namespace SmartButler.Logic.ModelViewModels
{
	public class IngredientViewModel : BaseViewModel
	{
		private string _name;
		private int _milliliter;
		private int _bottleIndex;
		private byte[] _byteImage;

		public IngredientViewModel(Ingredient ingredient)
		{
			Ingredient = ingredient;

			Name = ingredient.Name;
			BottleIndex = ingredient.BottleIndex;
			ByteImage = ingredient.ByteImage;
		}

		public IngredientViewModel(Ingredient ingredient, DrinkIngredient drinkIngredient)
		{
			Ingredient = ingredient;
			DrinkIngredient = drinkIngredient;

			Name = ingredient.Name;
			BottleIndex = ingredient.BottleIndex;
			ByteImage = ingredient.ByteImage;

			Milliliter = drinkIngredient.Milliliter;
		}

		public Ingredient Ingredient { get; private set; }
		public DrinkIngredient DrinkIngredient { get; private set; }

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

		public void UpdateIngredientModel()
		{
			Ingredient.Name = Name;
			Ingredient.BottleIndex = BottleIndex;
			Ingredient.ByteImage = ByteImage;
		}

		public void UpdateDrinkIngredientModel()
		{
			Ingredient.Name = Name;
			Ingredient.BottleIndex = BottleIndex;

			DrinkIngredient.Milliliter = Milliliter;
		}

	}
}
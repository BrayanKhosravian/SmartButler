using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reactive;
using System.Windows.Input;
using Plugin.Media;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Extensions;
using SmartButler.Logic.Common;
using SmartButler.Logic.EqualityComparer;
using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.ModelViewModels
{
	public class DrinkIngredientViewModel : DrinkIngredientViewModelBase
	{
		private string _name;
		private int _milliliter;
		private int _bottleIndex;
		private byte[] _byteImage;
		private bool _isDefault;
		private bool _isMilliliterValid;

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
		public DrinkIngredient DrinkIngredient { get; private set; }

		public string Name
		{
			get => _name;
			set => SetValue(ref _name, value);
		}
		
		public int Milliliter
		{
			get => _milliliter;
			set 
			{
				this.SetValue(ref _milliliter, value);
				IsMilliliterValid = Milliliter.IsInputValid();
				base.OnPropertyChanged(nameof(IsMilliliterValid));
			}
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

		public bool IsMilliliterValid
		{
			get => _isMilliliterValid;
			set => this.RaiseAndSetIfChanged(ref _isMilliliterValid, value);
		}


		public Unit TickSelected
		{
			get => Unit.Default;
			set => this.RaisePropertyChanged();
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
			if(DrinkIngredient == null)
				DrinkIngredient = new DrinkIngredient();
			DrinkIngredient.Milliliter = Milliliter;
			DrinkIngredient.Ingredient = Ingredient;
			// map adapter table to adapter // adapter table = DrinkIngredient, adapter = Ingredient 
			DrinkIngredient.IngredientId = Ingredient.Id; 
		}

		public static IEqualityComparer<DrinkIngredientViewModel> DrinkIngredientViewModelComparer { get; } = new DrinkIngredientViewModelEqualityComparer();

		internal static readonly ArrayEqualityComparer<byte> ByteArrayEqualityComparer = new ArrayEqualityComparer<byte>();

		private sealed class DrinkIngredientViewModelEqualityComparer : IEqualityComparer<DrinkIngredientViewModel>
		{
			public bool Equals(DrinkIngredientViewModel x, DrinkIngredientViewModel y)
			{
				if (ReferenceEquals(x, y)) return true;
				if (ReferenceEquals(x, null)) return false;
				if (ReferenceEquals(y, null)) return false;
				if (x.GetType() != y.GetType()) return false;
				return x._name == y._name &&
				       ByteArrayEqualityComparer.Equals(x.ByteImage, y.ByteImage) && 
				       x._isDefault == y._isDefault;
			}

			public int GetHashCode(DrinkIngredientViewModel obj)
			{
				unchecked
				{
					var hashCode = (obj._name != null ? obj._name.GetHashCode() : 0);
					hashCode = (hashCode * 397) ^ ByteArrayEqualityComparer.GetHashCode(obj.ByteImage);
					hashCode = (hashCode * 397) ^ obj._isDefault.GetHashCode();
					return hashCode;
				}
			}
		}

	}
}
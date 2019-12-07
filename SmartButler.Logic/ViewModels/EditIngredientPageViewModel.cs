using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;

namespace SmartButler.Logic.ViewModels
{
	public class EditIngredientPageViewModel : BaseViewModel
	{
		private byte[] _ingredientImage;
		private string _ingredientName;
		private int _selectedBottleIndex;

		private readonly IIngredientsRepository _ingredientsRepository;
		private readonly INavigationService _navigationService;
		private readonly IUserInteraction _userInteraction;
		private int _bottleIndex;

		public EditIngredientPageViewModel(IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			IUserInteraction userInteraction,
			Ingredient ingredient)
		{
			_ingredientsRepository = ingredientsRepository;
			_navigationService = navigationService;
			_userInteraction = userInteraction;
			Ingredient = ingredient;

			IngredientImage = ingredient.ByteImage;
			IngredientName = ingredient.Name;
			BottleIndex = ingredient.BottleIndex;
			SelectedBottleIndex = ingredient.BottleIndex;

			AbortCommand = new DelegateCommand( async _ => await _navigationService.PopAsync());
			AcceptCommand = new DelegateCommand(async _ =>
			{
				if (!await IsInputValidAsync()) return;

				Ingredient.Name = _ingredientName;
				Ingredient.ByteImage = _ingredientImage;
				Ingredient.BottleIndex = _bottleIndex;

				await _ingredientsRepository.UpdateAsync(Ingredient);
				await _navigationService.PopAsync();
			});
		}

		public Ingredient Ingredient { get; }
		public List<AvailablePosition> AvailablePositions { get; } = new List<AvailablePosition>(GetAvailablePositions());
		public DelegateCommand AbortCommand { get; }
		public DelegateCommand AcceptCommand { get; }


		public byte[] IngredientImage
		{
			get => _ingredientImage;
			set => SetValue(ref _ingredientImage, value);
		}

		public string IngredientName
		{
			get => _ingredientName.Trim();
			set => SetValue(ref _ingredientName, value.Trim());
		}

		public int SelectedBottleIndex
		{
			get => _selectedBottleIndex;
			set => _selectedBottleIndex = value;
		}

		public int BottleIndex
		{
			get => _bottleIndex;
			set => SetValue(ref _bottleIndex, value);
		}


		private async Task<bool> IsInputValidAsync()
		{
			var result = false;
			var msgBuilder = new StringBuilder();

			if (string.IsNullOrEmpty(IngredientName) || IngredientName.Length < 3 || IngredientName.Length > 255)
				msgBuilder.Append("The name of the ingredient should have more then 3 or less then 255 characters!\n");
			else
				result = true;

			if (!result) await _userInteraction.DisplayAlertAsync("Error", msgBuilder.ToString(), "OK");

			return result;
		}


		public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }

		public void SetToolBarControlViewModel(ToolbarControlViewModel toolbar)
		{
			ToolbarControlViewModel = toolbar;
		}

		private static IEnumerable<AvailablePosition> GetAvailablePositions()
		{
			yield return new AvailablePosition(0, "Not selected");
			yield return new AvailablePosition(1, "1");
			yield return new AvailablePosition(2, "2");
			yield return new AvailablePosition(3, "3");
			yield return new AvailablePosition(4, "4");
			yield return new AvailablePosition(5, "5");
			yield return new AvailablePosition(6, "6");
		}

		public class AvailablePosition
		{
			public AvailablePosition(int key, string value)
			{
				Key = key;
				Value = value;
			}

			public int Key { get; set; }
			public string Value { get; set; }
		}
	}
}

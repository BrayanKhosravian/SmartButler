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
		private int _ingredientPosition;

		private readonly IIngredientsRepository _ingredientsRepository;
		private readonly INavigationService _navigationService;
		private readonly IUserInteraction _userInteraction;
		private Ingredient Ingredient { get; }

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
			IngredientPosition = ingredient.BottleIndex;

			AbortCommand = new DelegateCommand( async _=> await _navigationService.PopAsync());
			AcceptCommand = new DelegateCommand(async _ =>
			{
				if (!await IsInputValidAsync()) return;

				Ingredient.Name = _ingredientName;
				Ingredient.ByteImage = _ingredientImage;
				Ingredient.BottleIndex = _ingredientPosition;

				await _ingredientsRepository.UpdateAsync(Ingredient);
				await _navigationService.PopAsync();
			});
		}

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

		public int IngredientPosition
		{
			get => _ingredientPosition;
			set => SetValue(ref _ingredientPosition, value);
		}


		private async Task<bool> IsInputValidAsync()
		{
			var result = false;
			var msgBuilder = new StringBuilder();

			if (IngredientPosition < 1 || IngredientPosition > 6)
				msgBuilder.Append("The Bottle position should be between 1 and 6!\n");
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
	}
}

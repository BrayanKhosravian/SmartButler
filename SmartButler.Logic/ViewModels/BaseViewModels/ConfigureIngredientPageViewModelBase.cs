using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ReactiveUI;
using SmartButler.Framework.Extensions;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.ViewModels.BaseViewModels
{
	public abstract class ConfigureIngredientPageViewModelBase : ToolBarPageViewModelBase
	{
		private byte[] _ingredientImage;
		private string _ingredientName;
		private int _selectedBottleIndex;
		private int _bottleIndex;

		private readonly IUserInteraction _userInteraction;

		protected ConfigureIngredientPageViewModelBase(
			INavigationService navigationService, 
			IUserInteraction userInteraction,
			ICrossMediaService crossMediaService)
		{
			_userInteraction = userInteraction;

			AbortCommand = new Lazy<ReactiveCommand>(() => 
				ReactiveCommand.CreateFromTask( async _ => await navigationService.PopAsync()));
			
			ImageTappedCommand = ReactiveCommand.CreateFromTask(async _ => 
				IngredientImage = await crossMediaService.GetPhotoAsync());
		}

		public DrinkIngredientViewModel DrinkIngredientViewModel { get; protected set; }
		public ReactiveCommand AcceptCommand { get; protected set; }
		public Lazy<ReactiveCommand> AbortCommand { get; }
		public ReactiveCommand ImageTappedCommand { get; }

		protected async Task<bool> IsInputValidAsync()
		{
			var result = false;
			var msgBuilder = new StringBuilder();

			if (!IngredientName.IsInputValid())
				msgBuilder.Append("The name of the ingredient should have more then 5 or less then 250 characters!\n");
			else
				result = true;

			if (!result) await _userInteraction.DisplayAlertAsync("Error", msgBuilder.ToString(), "OK");

			return result;
		}

		public abstract string Title { get; }

		public byte[] IngredientImage
		{
			get => _ingredientImage;
			set => SetValue(ref _ingredientImage, value);
		}

		public string IngredientName
		{
			get => _ingredientName?.Trim();
			set => this.RaiseAndSetIfChanged(ref _ingredientName, value?.Trim());
		}

		public int SelectedBottleIndex
		{
			get => _selectedBottleIndex;
			set => SetValue(ref _selectedBottleIndex, value);
		}

		public int BottleIndex
		{
			get => _bottleIndex;
			set => SetValue(ref _bottleIndex, value);
		}

	}
}
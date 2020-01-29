using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class EditIngredientPageViewModel : BaseViewModel, IHasToolBarViewModel
	{
		private byte[] _ingredientImage;
		private string _ingredientName;
		private int _selectedBottleIndex;
		private string _title;
		private int _bottleIndex;

		private readonly IIngredientsRepository _ingredientsRepository;
		private readonly INavigationService _navigationService;
		private readonly IUserInteraction _userInteraction;
		

		// shared default ctor
		private EditIngredientPageViewModel()
		{
			AbortCommand = new Lazy<ReactiveCommand>(() => 
				ReactiveCommand.CreateFromTask( async _ => await _navigationService.PopAsync()));
			
			ImageTappedCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				await CrossMedia.Current.Initialize();

				var galleryOrCamera = await _userInteraction.DisplayActionSheetAsync("How to pick an image", 
					"cancel", null, "gallery", "camera");

				MediaFile file = null;
				switch (galleryOrCamera)
				{
					case "gallery":
						file = await CrossMedia.Current.PickPhotoAsync();
						break;
					case "camera":
						file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
						break;
					default:
						return;
				}
				
				if(file == null) return;

				var s = file.GetStream();
				_ingredientImage = new byte[s.Length];
				s.Read(_ingredientImage, 0, (int) s.Length);
				OnPropertyChanged(nameof(IngredientImage));
				file.Dispose();
			});
		}

		// edit an ingredient
		public EditIngredientPageViewModel(IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			IUserInteraction userInteraction,
			DrinkIngredientViewModel drinkIngredientViewModel) : this()
		{
			_ingredientsRepository = ingredientsRepository;
			_navigationService = navigationService;
			_userInteraction = userInteraction;
			DrinkIngredientViewModel = drinkIngredientViewModel;

			IngredientImage = drinkIngredientViewModel.ByteImage;
			IngredientName = drinkIngredientViewModel.Name;
			BottleIndex = drinkIngredientViewModel.BottleIndex;
			SelectedBottleIndex = drinkIngredientViewModel.BottleIndex;

			AcceptCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				if (!await IsInputValidAsync()) return;

				DrinkIngredientViewModel.Name = _ingredientName;
				DrinkIngredientViewModel.ByteImage = _ingredientImage;
				DrinkIngredientViewModel.BottleIndex = _bottleIndex;

				DrinkIngredientViewModel.UpdateIngredientModel();

				await _ingredientsRepository.UpdateAsync(DrinkIngredientViewModel.Ingredient);
				await _navigationService.PopAsync();
			});

			Title = "Edit your Ingredient!";
		}

		// create and add an ingredient
		public EditIngredientPageViewModel(
			IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			IUserInteraction userInteraction) : this()
		{
			_ingredientsRepository = ingredientsRepository;
			_navigationService = navigationService;
			_userInteraction = userInteraction;

			DrinkIngredientViewModel = new DrinkIngredientViewModel(new Ingredient());

			AcceptCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				if (!await IsInputValidAsync()) return;

				DrinkIngredientViewModel.Name = _ingredientName;
				DrinkIngredientViewModel.ByteImage = _ingredientImage;
				DrinkIngredientViewModel.BottleIndex = _bottleIndex;

				DrinkIngredientViewModel.UpdateIngredientModel();

				await _ingredientsRepository.InsertAsync(DrinkIngredientViewModel.Ingredient);
				await _navigationService.PopAsync();

			});

			Title = "Create an Ingredient!";
		}

		public DrinkIngredientViewModel DrinkIngredientViewModel { get; }
		public Lazy<ReactiveCommand> AbortCommand { get; } 
		public ReactiveCommand AcceptCommand { get; }
		public ReactiveCommand ImageTappedCommand { get; }


		public string Title
		{
			get => _title;
			set => SetValue(ref _title, value);
		}

		public byte[] IngredientImage
		{
			get => _ingredientImage;
			set => SetValue(ref _ingredientImage, value);
		}

		public string IngredientName
		{
			get => _ingredientName?.Trim();
			set => SetValue(ref _ingredientName, value?.Trim());
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
	}
}

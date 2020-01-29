using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ReactiveUI;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;

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
			IUserInteraction userInteraction)
		{
			_userInteraction = userInteraction;

			AbortCommand = new Lazy<ReactiveCommand>(() => 
				ReactiveCommand.CreateFromTask( async _ => await navigationService.PopAsync()));
			
			ImageTappedCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				await CrossMedia.Current.Initialize();

				var galleryOrCamera = await userInteraction.DisplayActionSheetAsync("How to pick an image", 
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

		public DrinkIngredientViewModel DrinkIngredientViewModel { get; protected set; }
		public ReactiveCommand AcceptCommand { get; protected set; }
		public Lazy<ReactiveCommand> AbortCommand { get; }
		public ReactiveCommand ImageTappedCommand { get; }

		protected async Task<bool> IsInputValidAsync()
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

		public abstract string Title { get; }

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

	}
}
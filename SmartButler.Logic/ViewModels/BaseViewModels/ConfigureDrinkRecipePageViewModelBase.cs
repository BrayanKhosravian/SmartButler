using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.Framework.Extensions;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace SmartButler.Logic.ViewModels.BaseViewModels
{
	public abstract class ConfigureDrinkRecipePageViewModelBase : ToolBarPageViewModelBase
	{
		private string _title;
		private string _drinkName;
		private byte[] _byteImage;
		public ISelectionHost<DrinkIngredientViewModel> SelectionHost { get; }
		private readonly IUserInteraction _userInteraction;
		private readonly INavigationService _navigationService;

		protected ConfigureDrinkRecipePageViewModelBase(
			IUserInteraction userInteraction, 
			INavigationService navigationService,
			IDrinkRecipeBuilder drinkRecipeBuilder,
			ICrossMediaService crossMediaService,
			ISelectionHost<DrinkIngredientViewModel> selectionHost)
		{
			_userInteraction = userInteraction;
			_navigationService = navigationService;
			SelectionHost = selectionHost;

			var addIngredientCanExecute = this.WhenAnyValue(vm => vm.DrinkIngredients.Count, 
				i => i < 6);

			addIngredientCanExecute
				.Where(canExecute => !canExecute)
				.SubscribeAsync(async () => await _userInteraction.DisplayAlertAsync("Info",
						"Cannot have more then 6 ingredients", "Ok"));

			var alreadySelectedParameter = new TypedParameter(typeof(IEnumerable<DrinkIngredientViewModel>), DrinkIngredients);

			AddIngredientCommand = ReactiveCommand.CreateFromTask(async _ => 
					await _navigationService.PushAsync<SelectIngredientsPageViewModel>(alreadySelectedParameter),
				addIngredientCanExecute);

			AbortCommand = ReactiveCommand.CreateFromTask(async _ => await _navigationService.PopAsync());

			CompleteCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				if (!IsDrinkValid(out StringBuilder msgBuilder))
				{
					await userInteraction.DisplayAlertAsync("Error", msgBuilder.ToString(), "Ok");
					return;
				}

				var ingredients = DrinkIngredients.Select(ivm =>
				{
					ivm.UpdateDrinkIngredientModel();
					return ivm.DrinkIngredient;
				}).ToList();

				await CompletedTemplateMethod(drinkRecipeBuilder, ingredients);
				await navigationService.PopAsync();

			});
			CompleteCommand.ThrownExceptions.Subscribe(exception => throw exception);
			ByteImageTapRecognizerCommand = ReactiveCommand.CreateFromTask(async _ =>
				ByteImage = await crossMediaService.GetPhotoAsync());
		}

		protected abstract Task CompletedTemplateMethod(IDrinkRecipeBuilder drinkRecipeBuilder,
			IList<DrinkIngredient> ingredients);

		public ReactiveList<DrinkIngredientViewModel> DrinkIngredients { get; set; } 
			= new ReactiveList<DrinkIngredientViewModel>(){ChangeTrackingEnabled = true};

		public ReactiveCommand ByteImageTapRecognizerCommand { get; }
		public ReactiveCommand AddIngredientCommand { get; protected set; }
		public ReactiveCommand CompleteCommand { get; protected set; }
		public ReactiveCommand AbortCommand { get; protected set; }

		public void OnAppearing()
		{
			if(SelectionHost?.IsAvailable == true)
				DrinkIngredients.Add(SelectionHost.Selection);
		}
		public void OnDisappearing() => SelectionHost?.Reset();

		protected bool IsDrinkValid(out StringBuilder msgBuilder)
		{
			msgBuilder = new StringBuilder();
			if (string.IsNullOrWhiteSpace(DrinkName) || DrinkName.Length < 5 || DrinkName.Length > 250)
				msgBuilder.Append("'Drink name' has to have at least 5 and a maximum of 250 characters! \n");
			if (DrinkIngredients.Any(d => d.Milliliter < 15 || d.Milliliter > 500))
				msgBuilder.Append("'Milliliter' has to be between 15 and 500");

			return msgBuilder.Length == 0;
		}

		public string Title
		{
			get => _title;
			set => SetValue(ref _title, value);
		}

		public string DrinkName
		{
			get => _drinkName;
			set => this.SetValue(ref _drinkName, value);
		}

		public byte[] ByteImage
		{
			get => _byteImage;
			set => this.SetValue(ref _byteImage, value);
		}
	}
}
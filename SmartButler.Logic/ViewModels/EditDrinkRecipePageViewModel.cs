using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using ReactiveUI;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.ViewModels
{
	public class EditDrinkRecipePageViewModel : BaseViewModel, IHasToolBarViewModel
	{
		private string _title;

		private readonly IUserInteraction _userInteraction;
		private readonly IDrinkRecipeBuilder _drinkRecipeBuilder;
		private readonly INavigationService _navigationService;
		private readonly IDrinkRecipesRepository _drinkRecipesRepository;
		private readonly DrinkRecipeViewModel _drinkIngredientViewModel;
		private string _drinkName;
		public ISelectionHost<DrinkIngredientViewModel> SelectionHost { get; }

		private enum EditMode{None, CreateNew, Edit}

		private readonly EditMode _editMode;

		// shared ctor
		private EditDrinkRecipePageViewModel()
		{
			var addIngredientCanExecute = this.WhenAnyValue(vm => vm.DrinkIngredients.Count, 
				i => i < 6);

			addIngredientCanExecute
				.Where(canExecute => !canExecute)
				.Subscribe(async _ =>
					await _userInteraction.DisplayAlertAsync("Info",
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
					await _userInteraction.DisplayAlertAsync("Error", msgBuilder.ToString(), "Ok");
					return;
				}

				var ingredients = DrinkIngredients.Select(ivm =>
				{
					ivm.UpdateDrinkIngredientModel();
					return ivm.DrinkIngredient;
				}).ToList();

				if (_editMode == EditMode.CreateNew)
				{
					var drink = _drinkRecipeBuilder // TODO: Add ByteImage!
						.Default()
						.SetName(DrinkName)
						.AddIngredients(ingredients.ToArray())
						.Build();

					await _drinkRecipesRepository.InsertWithChildrenAsync(drink);
					await _userInteraction.DisplayAlertAsync("Info", "Drink Added to the Database!", "Ok");
				}
				else if (_editMode == EditMode.Edit)
				{
					var drink = _drinkRecipeBuilder // TODO: Add ByteImage!
						.TakeDefault(_drinkIngredientViewModel.DrinkRecipe)
						.ClearIngredients()
						.SetName(DrinkName)
						.AddIngredients(ingredients.ToArray())
						.Build();

					await _drinkRecipesRepository.UpdateWithChildrenAsync(drink);
					await _userInteraction.DisplayAlertAsync("Info", "Drink updated in the Database!", "Ok");
				}

				await _navigationService.PopAsync();

			});
		}

		// Edit a drink
		public EditDrinkRecipePageViewModel(
			IUserInteraction userInteraction, 
			IDrinkRecipeBuilder drinkRecipeBuilder, 
			INavigationService navigationService, 
			IDrinkRecipesRepository drinkRecipesRepository,
			ISelectionHost<DrinkIngredientViewModel> selectionHost,
			DrinkRecipeViewModel drinkRecipeViewModel) : this()
		{
			_editMode = EditMode.Edit;

			_userInteraction = userInteraction;
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_navigationService = navigationService;
			_drinkRecipesRepository = drinkRecipesRepository;
			SelectionHost = selectionHost;
			_drinkIngredientViewModel = drinkRecipeViewModel;

			DrinkName = drinkRecipeViewModel.Name;
			DrinkIngredients.AddRange(drinkRecipeViewModel.IngredientViewModels);

		}

		// Create a drink
		public EditDrinkRecipePageViewModel(
			IUserInteraction userInteraction, 
			IDrinkRecipeBuilder drinkRecipeBuilder, 
			INavigationService navigationService, 
			IDrinkRecipesRepository drinkRecipesRepository,
			ISelectionHost<DrinkIngredientViewModel> selectionHost) : this()
		{
			_editMode = EditMode.CreateNew;

			_userInteraction = userInteraction;
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_navigationService = navigationService;
			_drinkRecipesRepository = drinkRecipesRepository;
			SelectionHost = selectionHost;

		}

		private bool IsDrinkValid(out StringBuilder msgBuilder)
		{
			msgBuilder = new StringBuilder();
			if (string.IsNullOrWhiteSpace(DrinkName) || DrinkName.Length < 5 || DrinkName.Length > 250)
				msgBuilder.Append("'Drink name' has to have at least 5 and a maximum of 250 characters! \n");
			if (DrinkIngredients.Any(d => d.Milliliter < 15 || d.Milliliter > 500))
				msgBuilder.Append("'Milliliter' has to be between 15 and 500");

			return msgBuilder.Length == 0;
		}


		public ReactiveList<DrinkIngredientViewModel> DrinkIngredients { get; set; } 
			= new ReactiveList<DrinkIngredientViewModel>();
		public ReactiveCommand AddIngredientCommand { get; }
		public ReactiveCommand CompleteCommand { get; }
		public ReactiveCommand AbortCommand { get; }

		public void OnAppearing()
		{
			if(SelectionHost != null && SelectionHost.IsAvailable)
				DrinkIngredients.Add(SelectionHost.Selection);
		}
		public void OnDisappearing() => SelectionHost?.Reset();

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

		public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
		public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
		{
			ToolbarControlViewModel = vm;
		}

	}
}

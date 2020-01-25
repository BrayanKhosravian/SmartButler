using System;
using System.Collections.Generic;
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
	public class EditDrinkRecipePageViewModel : BaseViewModel
	{
		private string _title;

		private readonly IUserInteraction _userInteraction;
		private readonly IDrinkRecipeBuilder _drinkRecipeBuilder;
		private readonly INavigationService _navigationService;
		private readonly IDrinkRecipesRepository _drinkRecipesRepository;
		public ISelectionHost<DrinkIngredientViewModel> SelectionHost { get; }

		public EditDrinkRecipePageViewModel(
			IUserInteraction userInteraction, 
			IDrinkRecipeBuilder drinkRecipeBuilder, 
			INavigationService navigationService, 
			IDrinkRecipesRepository drinkRecipesRepository,
			ISelectionHost<DrinkIngredientViewModel> selectionHost)
		{
			_userInteraction = userInteraction;
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_navigationService = navigationService;
			_drinkRecipesRepository = drinkRecipesRepository;
			SelectionHost = selectionHost;


			var navigationMode = IngredientsPageViewModel.NavigationMode.Select;
			var navigationParameter = new IngredientsPageViewModel.Parameter(navigationMode, DrinkIngredients);
			var parameter = new TypedParameter(typeof(IngredientsPageViewModel.Parameter), navigationParameter);

			AddDrinkCommand = ReactiveCommand.CreateFromTask(async _ => 
				await _navigationService.PushAsync<IngredientsPageViewModel>(parameter));
		}

		public ReactiveList<DrinkIngredientViewModel> DrinkIngredients { get; set; } 
			= new ReactiveList<DrinkIngredientViewModel>();
		public ReactiveCommand AddDrinkCommand { get; }

		public void OnAppearing()
		{
			if(SelectionHost.IsAvailable)
				DrinkIngredients.Add(SelectionHost.Selection);
		}
		public void OnDisappearing() => SelectionHost.Reset();

		public string Title
		{
			get => _title;
			set => SetValue(ref _title, value);
		}

		public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
		public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
		{
			ToolbarControlViewModel = vm;
		}

	}
}

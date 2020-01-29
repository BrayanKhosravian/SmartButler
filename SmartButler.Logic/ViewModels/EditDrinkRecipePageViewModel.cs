﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Bluetooth;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public abstract class ConfigureDrinkRecipePageViewModelBase : ToolBarPageViewModelBase
	{
		private string _title;
		private string _drinkName;
		public ISelectionHost<DrinkIngredientViewModel> SelectionHost { get; }

		protected ConfigureDrinkRecipePageViewModelBase(
			IUserInteraction userInteraction, 
			INavigationService navigationService,
			IDrinkRecipeBuilder drinkRecipeBuilder,
			ISelectionHost<DrinkIngredientViewModel> selectionHost)
		{
			SelectionHost = selectionHost;

			var addIngredientCanExecute = this.WhenAnyValue(vm => vm.DrinkIngredients.Count, 
				i => i < 6);

			addIngredientCanExecute
				.Where(canExecute => !canExecute)
				.Subscribe(async _ =>
					await userInteraction.DisplayAlertAsync("Info",
						"Cannot have more then 6 ingredients", "Ok"));

			var alreadySelectedParameter = new TypedParameter(typeof(IEnumerable<DrinkIngredientViewModel>), DrinkIngredients);

			AddIngredientCommand = ReactiveCommand.CreateFromTask(async _ => 
					await navigationService.PushAsync<SelectIngredientsPageViewModel>(alreadySelectedParameter),
				addIngredientCanExecute);

			AbortCommand = ReactiveCommand.CreateFromTask(async _ => await navigationService.PopAsync());

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
		}

		protected abstract Task CompletedTemplateMethod(IDrinkRecipeBuilder drinkRecipeBuilder,
			IList<DrinkIngredient> ingredients);

		public ReactiveList<DrinkIngredientViewModel> DrinkIngredients { get; set; } 
			= new ReactiveList<DrinkIngredientViewModel>();
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

	}

	public class AddDrinkRecipePageViewModel : ConfigureDrinkRecipePageViewModelBase
	{
		private readonly IUserInteraction _userInteraction;
		private readonly IDrinkRecipeBuilder _drinkRecipeBuilder;
		private readonly IDrinkRecipesRepository _drinkRecipesRepository;

		public AddDrinkRecipePageViewModel(
			IUserInteraction userInteraction, 
			INavigationService navigationService, 
			IDrinkRecipeBuilder drinkRecipeBuilder,
			IDrinkRecipesRepository drinkRecipesRepository, 
			ISelectionHost<DrinkIngredientViewModel> selectionHost) 
			: base(userInteraction, navigationService, drinkRecipeBuilder, selectionHost)
		{
			_userInteraction = userInteraction;
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_drinkRecipesRepository = drinkRecipesRepository;
		}

		protected override async Task CompletedTemplateMethod(IDrinkRecipeBuilder drinkRecipeBuilder,
			IList<DrinkIngredient> ingredients)
		{
			var drink = _drinkRecipeBuilder // TODO: Add ByteImage!
				.Default()
				.SetName(DrinkName)
				.AddIngredients(ingredients.ToArray())
				.Build();

			await _drinkRecipesRepository.InsertWithChildrenAsync(drink);
			await _userInteraction.DisplayAlertAsync("Info", "Drink Added to the Database!", "Ok");
		}
	}


	public class EditDrinkRecipePageViewModel : ConfigureDrinkRecipePageViewModelBase
	{
		private readonly IUserInteraction _userInteraction;
		private readonly IDrinkRecipeBuilder _drinkRecipeBuilder;
		private readonly IDrinkRecipesRepository _drinkRecipesRepository;
		private readonly DrinkRecipeViewModel _drinkIngredient;

		public EditDrinkRecipePageViewModel(
			IUserInteraction userInteraction, 
			IDrinkRecipeBuilder drinkRecipeBuilder, 
			INavigationService navigationService, 
			IDrinkRecipesRepository drinkRecipesRepository,
			ISelectionHost<DrinkIngredientViewModel> selectionHost,
			DrinkRecipeViewModel drinkRecipeViewModel) 
			: base(userInteraction, navigationService, drinkRecipeBuilder, selectionHost)
		{
			_userInteraction = userInteraction;
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_drinkRecipesRepository = drinkRecipesRepository;
			_drinkIngredient = drinkRecipeViewModel;

			DrinkName = drinkRecipeViewModel.Name;
			DrinkIngredients.AddRange(drinkRecipeViewModel.IngredientViewModels);
		}

		protected override async Task CompletedTemplateMethod(IDrinkRecipeBuilder drinkRecipeBuilder,
			IList<DrinkIngredient> ingredients)
		{
			var drink = _drinkRecipeBuilder // TODO: Add ByteImage!
				.TakeDefault(_drinkIngredient.DrinkRecipe)
				.ClearIngredients()
				.SetName(DrinkName)
				.AddIngredients(ingredients.ToArray())
				.Build();

			await _drinkRecipesRepository.UpdateWithChildrenAsync(drink);
			await _userInteraction.DisplayAlertAsync("Info", "Drink updated in the Database!", "Ok");
		}
	}
}

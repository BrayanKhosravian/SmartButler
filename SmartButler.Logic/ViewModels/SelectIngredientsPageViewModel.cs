using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Framework.Extensions;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class SelectIngredientsPageViewModel : IngredientsPageViewModelBase
	{
		private readonly IEnumerable<DrinkIngredientViewModel> _excludedIngredients;

		private readonly ISelectionHost<DrinkIngredientViewModel> _selectionHost;
		private readonly INavigationService _navigationService;
		public SelectIngredientsPageViewModel(
			IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			ISelectionHost<DrinkIngredientViewModel> selectionHost,
			IEnumerable<DrinkIngredientViewModel> alreadySelected) : base(navigationService, ingredientsRepository)
		{
			_excludedIngredients = alreadySelected;
			_navigationService = navigationService;
			_selectionHost = selectionHost;

			this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
				.Where(ingredient => ingredient != null)
				.ManySelect(async ingredientViewModel =>
				{
					selectionHost.Selection = await ingredientViewModel; 
					await Observable.FromAsync(async _ =>  await _navigationService.PopAsync())
						.ObserveOn(RxApp.MainThreadScheduler);
				})
				.Subscribe();

			var navigationPop = Observable.FromAsync(_ => _navigationService.PopAsync());

			this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
				.Where(ingredient => ingredient != null)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Do(ingredient => _selectionHost.Selection = ingredient)
				.Select(_ => navigationPop)
				.Switch()
				.Subscribe();


			//this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
			//	.Where(ingredient => ingredient != null)
			//	.ObserveOn(RxApp.MainThreadScheduler)
			//	.Do(ingredient => selectionHost.Selection = ingredient)
			//	.Select(_ => _navigationService.PopAsync().ToObservable())
			//	.Switch()
			//	.Subscribe();
		}


		public override bool IsAddIngredientButtonVisible => false;
		protected override List<DrinkIngredientViewModel> FilterIngredientsTemplateMethod(IList<Ingredient> ingredients)
		{
			var ingredientViewModels = ingredients
				.Select(ingredient => new DrinkIngredientViewModel(ingredient))
				.Except(_excludedIngredients, DrinkIngredientViewModel.DrinkIngredientViewModelComparer)
				.ToList();

			return ingredientViewModels;
		}
	}
}

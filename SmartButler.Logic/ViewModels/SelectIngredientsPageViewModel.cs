using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class SelectIngredientsPageViewModel : IngredientsPageViewModelBase, IHasToolBarViewModel
	{
		private readonly IEnumerable<DrinkIngredientViewModel> _excludedIngredients;
		public SelectIngredientsPageViewModel(
			IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			ISelectionHost<DrinkIngredientViewModel> selectionHost,
			IEnumerable<DrinkIngredientViewModel> alreadySelected) : base(navigationService, ingredientsRepository)
		{
			_excludedIngredients = alreadySelected;

			this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
				.Where(ingredient => ingredient != null)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(async ingredientViewModel =>
				{
					selectionHost.Selection = ingredientViewModel;
					await navigationService.PopAsync();
				});
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

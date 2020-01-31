using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Autofac;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Interfaces;
using ReactiveUI;
using SmartButler.Framework.Common;
using SmartButler.Logic.Common;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class ShowIngredientsPageViewModel : IngredientsPageViewModelBase
    {
	    public ShowIngredientsPageViewModel(
		    IIngredientsRepository ingredientsRepository,
		    INavigationService navigationService,
		    IUserInteraction userInteraction) 
		    : base(navigationService, ingredientsRepository)
	    {
		    
		    this.WhenAnyObservable(vm => vm.Ingredients.ItemChanged)
			    .ObserveOn(RxApp.MainThreadScheduler)
			    .Where(arg => arg.PropertyName == nameof(DrinkIngredientViewModel.TickSelected))
			    .Select(arg => (DrinkIngredientViewModel)arg.Sender)
			    .Do(ingredient => SelectedDrinkIngredient = ingredient)
			    .Subscribe(async ingredientViewModel =>
			    {
				    var selections = new List<string>(){"Edit"};
				    if (!ingredientViewModel.IsDefault)
					    selections.Add("Delete");

				    var result = await userInteraction.DisplayActionSheetAsync(
					    $"{ingredientViewModel.Name} selected!",
					    "Cancel",
					    null,
					    selections.ToArray());

				    if (result == "Edit")
				    {
					    var navigationParameter = new TypedParameter(typeof(DrinkIngredientViewModel), ingredientViewModel);
					    await navigationService.PushAsync<EditIngredientPageViewModel>(navigationParameter);
				    }
				    else if (result == "Delete")
				    {
					    await ingredientsRepository.DeleteAsync(ingredientViewModel.Ingredient);
					    await ActivateAsync();
				    }
				    else
					    ExceptionFactory.Get<NotImplementedException>("That selection is not jet implemented!");
			    });
	    }

	    public override bool IsAddIngredientButtonVisible => true;
	    protected override List<DrinkIngredientViewModel> FilterIngredientsTemplateMethod(IList<Ingredient> ingredients)
	    {
		    var ingredientViewModels = ingredients
			    .Select(ingredient => new DrinkIngredientViewModel(ingredient))
			    .Distinct(DrinkIngredientViewModel.DrinkIngredientViewModelComparer)
			    .ToList();

		    return ingredientViewModels;
	    }
    }
}


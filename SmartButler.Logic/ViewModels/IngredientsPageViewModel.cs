using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using ReactiveUI;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.ViewModels
{
    public class IngredientsPageViewModel : BaseViewModel
    {
	    public ReactiveList<DrinkIngredientBaseViewModel> Ingredients { get; private set; } = new ReactiveList<DrinkIngredientBaseViewModel>();

	    private DrinkIngredientViewModel _selectedDrinkIngredient;

	    private readonly IIngredientsRepository _ingredientsRepository;
	    private readonly INavigationService _navigationService;
	    private readonly IUserInteraction _userInteraction;

	    public IngredientsPageViewModel(IIngredientsRepository ingredientsRepository, 
		    INavigationService navigationService,
		    IUserInteraction userInteraction)
        {
	        _ingredientsRepository = ingredientsRepository;
	        _navigationService = navigationService;
	        _userInteraction = userInteraction;

	        this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
		        .Where(ingredient => ingredient != null)
		        .ObserveOn(RxApp.MainThreadScheduler)
		        .Subscribe( async ingredientViewModel =>
		        {
			        if (ingredientViewModel.IsDefault)
				        return;

			        var result = await _userInteraction.DisplayActionSheetAsync($"{ingredientViewModel.Name} selected!",
				        "Cancel",
				        null,
				        "Edit", "Delete");

			        if (result == "Edit")
				        await _navigationService.PushAsync<EditIngredientPageViewModel>(new TypedParameter(typeof(DrinkIngredientViewModel), ingredientViewModel));

			        else if (result == "Delete")
			        {
				        await _ingredientsRepository.DeleteAsync(ingredientViewModel.Ingredient);
				        await ActivateAsync();
			        }
		        });
	        AddIngredientCommand = ReactiveCommand.CreateFromTask(async _ =>
		        await _navigationService.PushAsync<EditIngredientPageViewModel>());

        }
	    public ReactiveCommand AddIngredientCommand { get; }

	    public async Task ActivateAsync()
	    {
		    using (Ingredients.SuppressChangeNotifications())
		    {
			    Ingredients.Clear();

			    var ingredients = await _ingredientsRepository.GetAllAsync();
			    var ingredientViewModels =
				    ingredients.Select(ingredient => new DrinkIngredientViewModel(ingredient)).ToList();

			    var orderedIngredients = ingredientViewModels.OrderBy(i => i.IsDefault).ToList();

			    Ingredients.Add(new DrinkIngredientInfoViewModel() {InfoText = "Default Ingredients"});
			    Ingredients.AddRange(orderedIngredients.Where(i => i.IsDefault));

			    Ingredients.Add(new DrinkIngredientInfoViewModel() {InfoText = "Custom Ingredients"});
			    Ingredients.AddRange(orderedIngredients.Where(i => !i.IsDefault));
		    }

	    }

	    public DrinkIngredientViewModel SelectedDrinkIngredient
	    {
		    get => _selectedDrinkIngredient;
		    set
		    {
			    _selectedDrinkIngredient = value;
				this.RaisePropertyChanged();
		    }
	    }

	    public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
	    public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

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
using SmartButler.Framework.Common;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.ViewModels
{
    public class IngredientsPageViewModel : BaseViewModel
    {
		public sealed class Parameter
		{
			public Parameter(NavigationMode navigationMode, 
				IEnumerable<DrinkIngredientViewModel> excludedIngredients)
			{
				NavigationMode = navigationMode;
				ExcludedIngredients = excludedIngredients;
			}

			public Parameter(NavigationMode navigationMode)
			{
				NavigationMode = navigationMode;
			}

			public NavigationMode NavigationMode { get; }
			public IEnumerable<DrinkIngredientViewModel> ExcludedIngredients { get; }
				= new List<DrinkIngredientViewModel>();

		}

		public enum NavigationMode{Add, Select}

	    public ReactiveList<DrinkIngredientBaseViewModel> Ingredients { get; private set; } = new ReactiveList<DrinkIngredientBaseViewModel>();

	    private DrinkIngredientViewModel _selectedDrinkIngredient;

	    private readonly IIngredientsRepository _ingredientsRepository;
	    private readonly INavigationService _navigationService;
	    private readonly IUserInteraction _userInteraction;
	    private readonly ISelectionHost<DrinkIngredientViewModel> _selectionHost;
	    private readonly IEnumerable<DrinkIngredientViewModel> _excludedIngredients = new List<DrinkIngredientViewModel>();
	    private bool _isAddIngredientButtonVisible = true;
	    private readonly NavigationMode _navigationMode;

	    public IngredientsPageViewModel(
		    IIngredientsRepository ingredientsRepository,
		    INavigationService navigationService,
		    IUserInteraction userInteraction,
		    ISelectionHost<DrinkIngredientViewModel> selectionHost,
		    Parameter parameter)
	    {
		    _ingredientsRepository = ingredientsRepository;
		    _navigationService = navigationService;
		    _userInteraction = userInteraction;
		    _selectionHost = selectionHost;

		    if (parameter.NavigationMode == NavigationMode.Select)
		    {
			    _excludedIngredients = parameter.ExcludedIngredients;
			    IsAddIngredientButtonVisible = false;

			    this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
				    .Where(ingredient => ingredient != null)
				    .ObserveOn(RxApp.MainThreadScheduler)
				    .Subscribe(async ingredientViewModel =>
				    {
					    _selectionHost.Selection = ingredientViewModel;
					    await _navigationService.PopAsync();
				    });
		    }

		    else if (parameter.NavigationMode == NavigationMode.Add)
		    {
			    this.WhenAnyValue(vm => vm.SelectedDrinkIngredient)
				    .Where(ingredient => ingredient != null)
				    .ObserveOn(RxApp.MainThreadScheduler)
				    .Subscribe(async ingredientViewModel =>
				    {
					    if (ingredientViewModel.IsDefault)
						    return;

					    var result = await _userInteraction.DisplayActionSheetAsync(
						    $"{ingredientViewModel.Name} selected!",
						    "Cancel",
						    null,
						    "Edit", "Delete");

					    if (result == "Edit")
						    await _navigationService.PushAsync<EditIngredientPageViewModel>(
							    new TypedParameter(typeof(DrinkIngredientViewModel), ingredientViewModel));

					    else if (result == "Delete")
					    {
						    await _ingredientsRepository.DeleteAsync(ingredientViewModel.Ingredient);
						    await ActivateAsync();
					    }
				    });

			    AddIngredientCommand = ReactiveCommand.CreateFromTask(async _ =>
				    await _navigationService.PushAsync<EditIngredientPageViewModel>());
		    }
	    }

	    private List<Ingredient> _ingredients;
	    public ReactiveCommand AddIngredientCommand { get; }

	    public async Task ActivateAsync()
	    {
		    using (Ingredients.SuppressChangeNotifications())
		    {
			    if (Ingredients == null || Ingredients.Count <= 0)
				    _ingredients = await _ingredientsRepository.GetAllAsync();

			    var ingredientViewModels = _ingredients
				    .Select(ingredient => new DrinkIngredientViewModel(ingredient))
				    .ToList();

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

	    public bool IsAddIngredientButtonVisible
	    {
		    get => _isAddIngredientButtonVisible;
		    set => SetValue(ref _isAddIngredientButtonVisible, value);
	    }

	    public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }

	    public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

using System;
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

namespace SmartButler.Logic.ViewModels
{
    public class IngredientsPageViewModel : BaseViewModel
    {
	    public ReactiveList<Ingredient> Ingredients { get; private set; } = new ReactiveList<Ingredient>();

	    private Ingredient _selectedIngredient;

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

	        this.WhenAnyValue(vm => vm.SelectedIngredient).Skip(1)
		        .Where(ingredient => ingredient != null)
		        .Subscribe( async ingredient =>
		        {
			       var result = await _userInteraction.DisplayActionSheetAsync($"{ingredient.Name} selected!",
				        "Cancel",
				        string.Empty,
				        "Edit");

			       if (result == "Edit")
				       await _navigationService.PushAsync<EditIngredientPageViewModel>(new TypedParameter(typeof(Ingredient), ingredient));

		        });


        }

	    public ReactiveCommand AddBottleCommand { get; set; }


	    public async Task ActivateAsync()
	    {
		    using (Ingredients.SuppressChangeNotifications())
		    {
				Ingredients.Clear();
				Ingredients.AddRange(await _ingredientsRepository.GetAllAsync());
			}
	    }

	    public Ingredient SelectedIngredient
        {
	        get => _selectedIngredient;
	        set => this.RaiseAndSetIfChanged(ref _selectedIngredient, value);
        }

	    public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.ViewModels
{
	public abstract class IngredientsPageViewModelBase : BaseViewModel
	{
		private DrinkIngredientViewModel _selectedDrinkIngredient;

		private readonly IIngredientsRepository _ingredientsRepository;
		protected IngredientsPageViewModelBase(INavigationService navigationService,
			IIngredientsRepository ingredientsRepository)
		{
			_ingredientsRepository = ingredientsRepository;

			AddIngredientCommand = ReactiveCommand.CreateFromTask(async _ =>
				await navigationService.PushAsync<EditIngredientPageViewModel>());
		}

		public ReactiveList<DrinkIngredientBaseViewModel> Ingredients { get; } = new ReactiveList<DrinkIngredientBaseViewModel>();
		public ReactiveCommand AddIngredientCommand { get; }

		public abstract bool IsAddIngredientButtonVisible { get; }
		protected abstract List<DrinkIngredientViewModel> FilterIngredientsTemplateMethod(IList<Ingredient> ingredients);

		public async Task ActivateAsync()
		{
			using (Ingredients.SuppressChangeNotifications())
			{
				Ingredients.Clear();
				var ingredients = await _ingredientsRepository.GetAllAsync();

				var ingredientViewModels = FilterIngredientsTemplateMethod(ingredients);

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
			set => this.RaiseAndSetIfChanged(ref _selectedDrinkIngredient, value);
		}

		public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
		public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
		{
			ToolbarControlViewModel = vm;
		}

	}
}
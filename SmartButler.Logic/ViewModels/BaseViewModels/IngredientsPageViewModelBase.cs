using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.ViewModels.BaseViewModels
{
	public abstract class IngredientsPageViewModelBase : ToolBarPageViewModelBase
	{
		private DrinkIngredientViewModel _selectedDrinkIngredient;

		private readonly IIngredientsRepository _ingredientsRepository;
		protected IngredientsPageViewModelBase(INavigationService navigationService,
			IIngredientsRepository ingredientsRepository)
		{
			_ingredientsRepository = ingredientsRepository;

			AddIngredientCommand = ReactiveCommand.CreateFromTask(async _ =>
				await navigationService.PushAsync<AddIngredientPageViewModel>());
		}

		public ReactiveList<DrinkIngredientViewModelBase> Ingredients { get; } = 
			new ReactiveList<DrinkIngredientViewModelBase>(){ChangeTrackingEnabled = true};
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

				var orderedIngredients = ingredientViewModels
					.OrderBy(i => !i.IsAvailable)
					.ThenBy(i => i.BottleIndex)
					.ToList();

				Ingredients.AddRange(orderedIngredients);
			}
		}

		public DrinkIngredientViewModel SelectedDrinkIngredient
		{
			get => _selectedDrinkIngredient;
			set => this.RaiseAndSetIfChanged(ref _selectedDrinkIngredient, value);
		}

	}
}
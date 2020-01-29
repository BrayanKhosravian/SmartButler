using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Core;
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

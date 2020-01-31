using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
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
			ICrossMediaService crossMediaService,
			ISelectionHost<DrinkIngredientViewModel> selectionHost) 
			: base(userInteraction, navigationService, drinkRecipeBuilder, crossMediaService, selectionHost)
		{
			_userInteraction = userInteraction;
			_drinkRecipeBuilder = drinkRecipeBuilder;
			_drinkRecipesRepository = drinkRecipesRepository;
		}

		protected override async Task CompletedTemplateMethod(IDrinkRecipeBuilder drinkRecipeBuilder,
			IList<DrinkIngredient> ingredients)
		{
			var drink = _drinkRecipeBuilder
				.Default()
				.SetName(DrinkName)
				.SetByteImage(ByteImage)
				.AddIngredients(ingredients.ToArray())
				.Build();

			await _drinkRecipesRepository.InsertWithChildrenAsync(drink);
			await _userInteraction.DisplayAlertAsync("Info", "Drink Added to the Database!", "Ok");
		}
	}
}
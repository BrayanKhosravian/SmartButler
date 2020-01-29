using System.Collections.Generic;
using System.Windows.Input;
using ReactiveUI;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class EditIngredientPageViewModel : ConfigureIngredientPageViewModelBase
	{
		// edit an ingredient
		public EditIngredientPageViewModel(
			IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			IUserInteraction userInteraction,
			ICrossMediaService crossMediaService,
			DrinkIngredientViewModel drinkIngredientViewModel) 
			: base(navigationService, userInteraction, crossMediaService)
		{
			DrinkIngredientViewModel = drinkIngredientViewModel;

			IngredientImage = drinkIngredientViewModel.ByteImage;
			IngredientName = drinkIngredientViewModel.Name;
			BottleIndex = drinkIngredientViewModel.BottleIndex;
			SelectedBottleIndex = drinkIngredientViewModel.BottleIndex;

			AcceptCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				if (!await IsInputValidAsync()) return;

				DrinkIngredientViewModel.Name = IngredientName;
				DrinkIngredientViewModel.ByteImage = IngredientImage;
				DrinkIngredientViewModel.BottleIndex = BottleIndex;

				DrinkIngredientViewModel.UpdateIngredientModel();

				await ingredientsRepository.UpdateAsync(DrinkIngredientViewModel.Ingredient);
				await navigationService.PopAsync();
			});
		}

		public override string Title => "Edit your Ingredient!";
	}
}

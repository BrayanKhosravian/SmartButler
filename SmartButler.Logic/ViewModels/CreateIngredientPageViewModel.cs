using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Interfaces;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;
using SmartButler.Logic.ViewModels.BaseViewModels;

namespace SmartButler.Logic.ViewModels
{
	public class AddIngredientPageViewModel : ConfigureIngredientPageViewModelBase
	{
		public AddIngredientPageViewModel(
			IIngredientsRepository ingredientsRepository,
			INavigationService navigationService,
			IUserInteraction userInteraction,
			ICrossMediaService crossMediaService) 
			: base(navigationService, userInteraction, crossMediaService)
		{
			DrinkIngredientViewModel = new DrinkIngredientViewModel(new Ingredient());

			AcceptCommand = ReactiveCommand.CreateFromTask(async _ =>
			{
				if (!await IsInputValidAsync()) return;

				DrinkIngredientViewModel.Name = IngredientName;
				DrinkIngredientViewModel.ByteImage = IngredientImage;
				DrinkIngredientViewModel.BottleIndex = BottleIndex;

				DrinkIngredientViewModel.UpdateIngredientModel();

				await ingredientsRepository.InsertAsync(DrinkIngredientViewModel.Ingredient);
				await navigationService.PopAsync();

			});
		}

		public override string Title => "Create an Ingredient!";
	}
}
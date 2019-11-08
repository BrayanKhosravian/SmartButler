using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;
using ReactiveUI;

namespace SmartButler.Logic.ViewModels
{
    public class IngredientsPageViewModel : BaseViewModel
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        public ReactiveList<Ingredient> Bottles { get; private set; } = new ReactiveList<Ingredient>();

        public IngredientsPageViewModel(IIngredientsRepository ingredientsRepository, INavigationService navigationService)
        {
	        _ingredientsRepository = ingredientsRepository;

            AddBottleCommand =  ReactiveCommand.Create(() => { /*  Add bottle here */} );

        }

        public ReactiveCommand AddBottleCommand { get; set; }

        public async Task ActivateAsync()
        {
           if(!Bottles.Any())
               Bottles.AddRange(await _ingredientsRepository.GetAllAsync());
        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

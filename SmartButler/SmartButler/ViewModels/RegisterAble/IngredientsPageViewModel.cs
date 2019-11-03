using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SmartButler.Models;
using SmartButler.Repositories;
using SmartButler.Services.RegisterAble;
using Xamarin.Forms;

namespace SmartButler.ViewModels.RegisterAble
{
    public class IngredientsPageViewModel : BaseViewModel
    {
        private readonly IIngredientRepository _ingredientRepository;

        public ReactiveList<Ingredient> Bottles { get; private set; } = new ReactiveList<Ingredient>();

        public IngredientsPageViewModel(IIngredientRepository ingredientRepository, INavigationService navigationService)
        {
	        _ingredientRepository = ingredientRepository;

            AddBottleCommand = new Command(() => { /*  Add bottle here */} );

        }

        public ICommand AddBottleCommand { get; set; }

        public async Task ActivateAsync()
        {
           if(!Bottles.Any())
               Bottles.AddRange(await _ingredientRepository.GetAllAsync());
        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

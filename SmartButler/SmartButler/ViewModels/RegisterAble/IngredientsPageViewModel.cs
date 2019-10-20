using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using ReactiveUI;
using SmartButler.Models;
using SmartButler.Services.RegisterAble;
using SmartButler.Services.Registrable;
using Xamarin.Forms;

namespace SmartButler.ViewModels
{
    public class IngredientsPageViewModel : BaseViewModel
    {
        private readonly IIngredientBuilder _ingredientBuilder;

        public ReactiveList<Ingredient> Bottles { get; private set; } = new ReactiveList<Ingredient>();

        public IngredientsPageViewModel(IIngredientBuilder ingredientBuilder, INavigationService navigationService)
        {
            _ingredientBuilder = ingredientBuilder;

            AddBottleCommand = new Command(() => { /*  Add bottle here */} );

        }


        public ICommand AddBottleCommand { get; set; }

        public void Activate()
        {
           if(!Bottles.Any())
               Bottles.AddRange(DefaultBottles());
        }

        private IEnumerable<Ingredient> DefaultBottles()
        {
            var resolvingType = typeof(IngredientsPageViewModel);

            yield return _ingredientBuilder.Default("Whisky", "Bottles.Whisky.jpeg", resolvingType).Build();
			yield return _ingredientBuilder.Default("Vodka", "Bottles.Vodka.jpg", resolvingType).Build();
			yield return _ingredientBuilder.Default("Orange-Juice", "Bottles.OrangeJuice.jpg", resolvingType).Build();
			yield return _ingredientBuilder.Default("Cranberry-Juice", "Bottles.CranberryJuice.jpg", resolvingType).Build();
			yield return _ingredientBuilder.Default("Lemon-Juice", "Bottles.LemonJuice.jpg", resolvingType).Build();
			yield return _ingredientBuilder.Default("NONE", "NONE", resolvingType).Build();
		}

		public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

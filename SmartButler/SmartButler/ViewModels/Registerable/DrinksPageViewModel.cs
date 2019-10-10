using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;
using SmartButler.Models;
using SmartButler.Services.Registrable;

namespace SmartButler.ViewModels
{
    public class DrinksPageViewModel : BaseViewModel
    {
        public ReactiveList<Drink> Drinks { get; private set; } = new ReactiveList<Drink>();

        private readonly ILiquidContainerFactory _liquidContainerFactory;

        public DrinksPageViewModel(ILiquidContainerFactory liquidContainerFactory, INavigationService navigationService) 
        {
            _liquidContainerFactory = liquidContainerFactory;

           
        }


        public void Activate()
        {
            if(!Drinks.Any())
                Drinks.AddRange(AddDefaultDrinks());
        }

        private IEnumerable<Drink> AddDefaultDrinks()
        {
            var resolvingType = typeof(DrinksPageViewModel);
            yield return _liquidContainerFactory.Get<Drink>("Madras", "Drinks.Madras.jpg", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Screwdriver", "Drinks.Screwdriver.JPG", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Lemon Drop", "Drinks.Lemondrop.JPG", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Whisky Sour", "Drinks.WhiskySour.JPG", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Blizzard", "Drinks.Blizzard.JPG", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Cape Cod", "Drinks.CapeCod.JPG", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Hot Toddy", "Drinks.HotToddy.JPG", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Bourbon Squash", "Drinks.BourbonSquash.JPG", resolvingType);

        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

    }
}

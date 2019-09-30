using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using SmartButler.Models;
using SmartButler.Services.Registrable;

namespace SmartButler.ViewModels
{
    public class DrinksPageViewModel : BaseViewModel
    {
        public ReactiveList<LiquidContainer> Drinks { get; private set; } = new ReactiveList<LiquidContainer>();

        private readonly ILiquidContainerFactory _liquidContainerFactory;

        public DrinksPageViewModel(ILiquidContainerFactory liquidContainerFactory)
        {
            _liquidContainerFactory = liquidContainerFactory;
        }

        private IEnumerable<Drink> AddDefaultDrinks()
        {
            var resolvingType = typeof(DrinksPageViewModel);
            yield return _liquidContainerFactory.Get<Drink>("Madras", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Screwdriver", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Lemon Drop", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Whisky Sour", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Blizzard", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Cape Cod", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Hot Toddy", "Drinks.", resolvingType);
            yield return _liquidContainerFactory.Get<Drink>("Bourbon Squash", "Drinks.", resolvingType);

        }


    }
}

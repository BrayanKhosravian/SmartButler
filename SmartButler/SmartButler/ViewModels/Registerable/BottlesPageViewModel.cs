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
using SmartButler.Services.Registrable;
using Xamarin.Forms;

namespace SmartButler.ViewModels
{
    public class BottlesPageViewModel : BaseViewModel
    {
        private readonly ILiquidContainerFactory _liquidContainerFactory;

        public ReactiveList<Bottle> Bottles { get; private set; } = new ReactiveList<Bottle>();

        public BottlesPageViewModel(ILiquidContainerFactory liquidContainerFactory, INavigationService navigationService)
        {
            _liquidContainerFactory = liquidContainerFactory;

            AddBottleCommand = new Command(() => { /*  Add bottle here */} );

        }


        public ICommand AddBottleCommand { get; set; }

        public void Activate()
        {
           if(!Bottles.Any())
               Bottles.AddRange(DefaultBottles());
        }

        private IEnumerable<Bottle> DefaultBottles()
        {
            var resolvingType = typeof(BottlesPageViewModel);
            yield return _liquidContainerFactory.Get<Bottle>("Whisky", "Bottles.Whisky.jpeg", resolvingType);
            yield return _liquidContainerFactory.Get<Bottle>("Vodka", "Bottles.Vodka.jpg", resolvingType);
            yield return _liquidContainerFactory.Get<Bottle>("Orange-Juice", "Bottles.OrangeJuice.jpg", resolvingType);
            yield return _liquidContainerFactory.Get<Bottle>("Cranberry-Juice", "Bottles.CranberryJuice.jpg", resolvingType);
            yield return _liquidContainerFactory.Get<Bottle>("Lemon-Juice","Bottles.LemonJuice.jpg", resolvingType);
            yield return _liquidContainerFactory.Get<Bottle>("NONE", "NONE", resolvingType);
        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }


    }
}

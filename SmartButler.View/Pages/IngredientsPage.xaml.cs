using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;
using SmartButler.Logic.ViewModels.BaseViewModels;
using SmartButler.View.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientsPage : ContentPage, IViewFor<IngredientsPageViewModelBase>
	{
		public IngredientsPage ()
		{
			InitializeComponent ();

		    this.WhenActivated(  async closer =>
		    {
			    this.WhenAnyObservable(view => view.ViewModel.Ingredients.ItemChanged)
				    .ObserveOn(RxApp.MainThreadScheduler)
				    .Where(arg => arg.PropertyName == nameof(DrinkIngredientViewModel.TickSelected))
				    .Select(arg => (DrinkIngredientViewModel)arg.Sender)
				    .Do(selectedIngredient => ViewModel.SelectedDrinkIngredient = selectedIngredient)
				    .Subscribe()
				    .DisposeWith(closer);

			    if (ViewModel != null) await ViewModel.ActivateAsync();
		    });
		}

		object IViewFor.ViewModel
	    {
	        get => ViewModel;
	        set => ViewModel = value as IngredientsPageViewModelBase;
	    }

	    public IngredientsPageViewModelBase ViewModel
	    {
	        get => BindingContext as IngredientsPageViewModelBase;
	        set => BindingContext = value as IngredientsPageViewModelBase;
	    }

	    private void IngredientSelected(object sender, SelectedItemChangedEventArgs e)
	    {
			if(e is null) return;

			//EditIngredientView.IsVisible = true;

			// TODO: Uncomment if animating didnt work
			var ingredientViewModel = e.SelectedItem as DrinkIngredientViewModel;

			ViewModel.SelectedDrinkIngredient = ingredientViewModel;

			((ListView)sender).SelectedItem = null;


		}


	    //private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    //{
		   // EditIngredientView.IsVisible = false;
	    //}
	}
}
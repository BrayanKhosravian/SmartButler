using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;
using SmartButler.View.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinksPage : ContentPage, IViewFor<Logic.ViewModels.DrinksPageViewModel>
    {
        public DrinksPage()
        {
            InitializeComponent();

            this.WhenActivated(async closer =>
            {
	            if (ViewModel != null) await ViewModel?.Activate();
            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as Logic.ViewModels.DrinksPageViewModel;
        }

        public Logic.ViewModels.DrinksPageViewModel ViewModel
        {
            get => BindingContext as Logic.ViewModels.DrinksPageViewModel;
            set => BindingContext = value as Logic.ViewModels.DrinksPageViewModel;
        }

        private async void IngredientTappedRecognizer(object sender, EventArgs e)
        {
	        if (!(sender is VisualElement visual)) return;

	        var drinkRecipeViewModel = (DrinkRecipeViewModel)((BindableObject) sender).BindingContext;

	        try
	        {
                var tasks = new Func<List<Task>>(() => new List<Task>()
                {
	                AnimationService.VisualElementClicked(visual),
	                ViewModel.DrinkSelectedAsync(drinkRecipeViewModel)
                });

		        await Task.WhenAll(tasks.Invoke());
	        }
	        catch (Exception exception)
	        {
		        Console.WriteLine(exception);
		        await DisplayAlert("Exception!", $"{exception.Message}\n\n Try it again or restart the app!", "Ok");
	        }

        }
    }
}
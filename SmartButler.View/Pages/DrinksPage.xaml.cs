using System;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;
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

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
	        //ViewModel?.Transmit((DrinkRecipeViewModel)e.SelectedItem);
        }

        private async void DrinkTappedRecognizer(object sender, EventArgs e)
        {
            if(sender is null) return;
	        var drinkRecipeViewModel = (DrinkRecipeViewModel)((BindableObject) sender).BindingContext;
	        // await ViewModel.DrinkSelectedAsync(drinkIngredientViewModel);
	        await ExecuteSafeASync(async () => await ViewModel.DrinkSelectedAsync(drinkRecipeViewModel));

        }

        private async Task ExecuteSafeASync(Func<Task> task)
        {
	        try
	        {
		        await task.Invoke();
	        }
	        catch (Exception e)
	        {
		        Console.WriteLine(e);
		        throw;
	        }
        }
    }
}
using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinksPage : ContentPage, IViewFor<DrinksPageViewModel>
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
            set => ViewModel = value as DrinksPageViewModel;
        }

        public DrinksPageViewModel ViewModel
        {
            get => BindingContext as DrinksPageViewModel;
            set => BindingContext = value as DrinksPageViewModel;
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
	        //ViewModel?.Transmit((DrinkRecipe)e.SelectedItem);
        }
    }
}
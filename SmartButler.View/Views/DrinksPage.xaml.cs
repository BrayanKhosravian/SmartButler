using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using SmartButler.Logic.ViewModels;

namespace SmartButler.View.Views
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

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            

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

        private void Tapped(object sender, EventArgs e)
        {
	        
        }
    }
}
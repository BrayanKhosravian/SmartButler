using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinksPage : ContentPage, IViewFor<DrinksPageViewModel>
    {
        public DrinksPage()
        {
            InitializeComponent();

            this.WhenActivated(closer => { ViewModel?.Activate(); });
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
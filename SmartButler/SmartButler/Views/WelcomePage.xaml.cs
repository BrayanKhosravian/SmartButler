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
    public partial class WelcomePage : ContentPage, IViewFor<WelcomePageViewModel>
    {
        public WelcomePage()
        {
            InitializeComponent();

            this.WhenActivated(disposer =>
            {
                ViewModel?.Activate();
            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as WelcomePageViewModel;
        }

        public WelcomePageViewModel ViewModel
        {
            get => BindingContext as WelcomePageViewModel;
            set => BindingContext = value;
        }
    }
}
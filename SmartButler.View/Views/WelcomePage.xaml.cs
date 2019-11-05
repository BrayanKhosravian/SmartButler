using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using SmartButler.Logic.ViewModels;

namespace SmartButler.View.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : IViewFor<WelcomePageViewModel>
    {
        public WelcomePage()
        {
            InitializeComponent();

            this.WhenActivated(closer =>
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
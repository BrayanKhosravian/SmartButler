using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : IViewFor<Logic.ViewModels.WelcomePageViewModel>
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
            set => ViewModel = value as Logic.ViewModels.WelcomePageViewModel;
        }

        public Logic.ViewModels.WelcomePageViewModel ViewModel
        {
            get => BindingContext as Logic.ViewModels.WelcomePageViewModel;
            set => BindingContext = value;
        }
    }
}
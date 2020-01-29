using ReactiveUI;
using SmartButler.Logic.ViewModels;
using SmartButler.Logic.ViewModels.BaseViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditDrinkRecipePage : ContentPage, IViewFor<ConfigureDrinkRecipePageViewModelBase>
	{
		public EditDrinkRecipePage()
		{
			InitializeComponent();

			this.WhenActivated(cleaner =>
			{
				
			});
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as ConfigureDrinkRecipePageViewModelBase;
		}

		public ConfigureDrinkRecipePageViewModelBase ViewModel
		{
			get => BindingContext as ConfigureDrinkRecipePageViewModelBase; 
			set => BindingContext = value;
		}

		protected override void OnAppearing()
		{
			ViewModel.OnAppearing();
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			ViewModel.OnDisappearing();
			base.OnDisappearing();
		}
	}
}
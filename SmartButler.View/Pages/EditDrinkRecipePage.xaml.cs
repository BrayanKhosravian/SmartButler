using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditDrinkRecipePage : ContentPage, IViewFor<Logic.ViewModels.EditDrinkRecipePageViewModel>
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
			set => ViewModel = value as Logic.ViewModels.EditDrinkRecipePageViewModel;
		}

		public Logic.ViewModels.EditDrinkRecipePageViewModel ViewModel
		{
			get => BindingContext as Logic.ViewModels.EditDrinkRecipePageViewModel; 
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Logic.ModelViewModels;
using SmartButler.View.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Cells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientViewCell : ViewCell, IViewFor<DrinkIngredientViewModel>
	{
		public IngredientViewCell()
		{
			InitializeComponent();
			this.WhenActivated(vm =>
			{

			});
		}

		private async void IngredientTappedRecognizer(object sender, EventArgs e)
		{
			var visual = sender as VisualElement;
			if (visual is null) return;

			ViewModel.TickSelected = Unit.Default;

			await AnimationService.VisualElementClicked(visual);
		}

		private async void Cell_OnAppearing(object sender, EventArgs e)
		{
			var visual = sender as VisualElement;
			if (visual is null) return;

			await AnimationService.MakeVisibleAsync(visual);
		}

		private void Cell_OnDisappearing(object sender, EventArgs e)
		{
		    
		}

		private void IngredientViewCell_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as DrinkIngredientViewModel;
		}

		public DrinkIngredientViewModel ViewModel
		{
			get => BindingContext as DrinkIngredientViewModel;
			set => BindingContext = value;
		}
	}

	
}
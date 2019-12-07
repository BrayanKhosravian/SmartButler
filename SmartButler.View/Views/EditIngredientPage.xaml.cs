using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditIngredientPage : ContentPage, IViewFor<EditIngredientPageViewModel>
	{
		public EditIngredientPage()
		{
			InitializeComponent();

			this.WhenActivated(cleaner =>
			{
				var position = ViewModel.BottleIndex;
				IngredientPositionPicker.SelectedIndex = position;
			});
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as EditIngredientPageViewModel;
		}

		public EditIngredientPageViewModel ViewModel
		{
			get => BindingContext as EditIngredientPageViewModel;
			set => BindingContext = value;
		}

		private void IngredientPosition_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
				ViewModel.BottleIndex = selectedIndex;
		}

	}
}
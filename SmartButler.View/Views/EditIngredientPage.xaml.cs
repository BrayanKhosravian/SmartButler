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

			var positions = new List<int>() {1, 2, 3, 4, 5, 6};

			this.WhenActivated(cleaner =>
			{
				IngredientPositionPicker.ItemsSource = positions;

				var pos = ViewModel.IngredientPosition;
				IngredientPositionPicker.SelectedIndex = pos - 1;

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
			var picker = (Picker) sender;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
			{
				//picker.SelectedItem = picker.ItemsSource[selectedIndex];
			}
		}

		private void Picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{

		}
	}
}
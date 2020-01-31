using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SmartButler.Framework.Extensions;
using SmartButler.Logic.ViewModels;
using SmartButler.Logic.ViewModels.BaseViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfigureIngredientPage : ContentPage, IViewFor<ConfigureIngredientPageViewModelBase>
	{
		public ConfigureIngredientPage()
		{
			InitializeComponent();

			IngredientPositionPicker.ItemsSource = AvailablePositions;

			this.WhenActivated(cleaner =>
			{
				var position = ViewModel.BottleIndex;
				IngredientPositionPicker.SelectedIndex = position;

				this.WhenAnyValue(view => view.IngredientName.Text)
					.ObserveOn(RxApp.MainThreadScheduler)
					.Subscribe(name => 
						IngredientName.BackgroundColor = name.IsInputValid() ? Color.Default : Color.LightCoral);
			});
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as ConfigureIngredientPageViewModelBase;
		}

		public ConfigureIngredientPageViewModelBase ViewModel
		{
			get => BindingContext as ConfigureIngredientPageViewModelBase;
			set => BindingContext = value;
		}

		private void IngredientPosition_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;

			if (selectedIndex != -1)
				ViewModel.BottleIndex = selectedIndex;
		}

		private List<AvailablePosition> AvailablePositions { get; } = new List<AvailablePosition>(GetAvailablePositions());

		private static IEnumerable<AvailablePosition> GetAvailablePositions()
		{
			yield return new AvailablePosition(0, "Not selected");
			yield return new AvailablePosition(1, "1");
			yield return new AvailablePosition(2, "2");
			yield return new AvailablePosition(3, "3");
			yield return new AvailablePosition(4, "4");
			yield return new AvailablePosition(5, "5");
			yield return new AvailablePosition(6, "6");
		}

		internal class AvailablePosition
		{
			public AvailablePosition(int key, string value)
			{
				Key = key;
				Value = value;
			}

			public int Key { get; set; }
			public string Value { get; set; }
		}
	}
}
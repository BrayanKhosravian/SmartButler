﻿using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SmartButler.Framework.Extensions;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;
using SmartButler.Logic.ViewModels.BaseViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfigureDrinkRecipePage : ContentPage, IViewFor<ConfigureDrinkRecipePageViewModelBase>
	{
		public ConfigureDrinkRecipePage()
		{
			InitializeComponent(); 

			this.WhenActivated(cleaner =>
			{
				this.WhenAnyValue(view => view.DrinkName.Text)
					.ObserveOn(RxApp.MainThreadScheduler)
					.Subscribe(name =>
						DrinkName.BackgroundColor = name.IsInputValid() ? Color.Default : Color.LightCoral);

				//this.WhenAnyObservable(view => view.ViewModel.DrinkIngredients.ItemChanged)
				//	.ObserveOn(RxApp.MainThreadScheduler)
				//	.Where(ingredientChanged =>
				//		ingredientChanged.PropertyName.Equals(nameof(DrinkIngredientViewModel.Milliliter)))
				//	.Select(ingredientChanged => ingredientChanged.Sender)
				//	.Subscribe(ingredient => ingredient.IsMilliliterValid =
				//		ingredient.Milliliter.ToString().IsMilliliterValid());



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
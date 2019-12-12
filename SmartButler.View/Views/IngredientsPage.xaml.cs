﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.ViewModels;

namespace SmartButler.View.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientsPage : ContentPage, IViewFor<IngredientsPageViewModel>
	{
		public IngredientsPage ()
		{
			InitializeComponent ();

		    this.WhenActivated(  async closer =>
		    {
			    if (ViewModel != null) await ViewModel.ActivateAsync();
		    });
		}

		object IViewFor.ViewModel
	    {
	        get => ViewModel;
	        set => ViewModel = value as IngredientsPageViewModel;
	    }

	    public IngredientsPageViewModel ViewModel
	    {
	        get => BindingContext as IngredientsPageViewModel;
	        set => BindingContext = value as IngredientsPageViewModel;
	    }

	    private void IngredientSelected(object sender, SelectedItemChangedEventArgs e)
	    {
			if(e is null) return;

		    var ingredientViewModel = e.SelectedItem as IngredientViewModel;

		    ViewModel.SelectedIngredient = ingredientViewModel;

		    ((ListView) sender).SelectedItem = null;


	    }
	}
}
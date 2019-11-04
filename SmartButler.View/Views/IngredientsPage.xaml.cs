using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
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


	    private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        

	    }

	    protected override void OnBindingContextChanged()
	    {
	        base.OnBindingContextChanged();
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
	}
}
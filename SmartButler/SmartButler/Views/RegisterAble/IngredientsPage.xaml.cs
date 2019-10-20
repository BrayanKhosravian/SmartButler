using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IngredientsPage : ContentPage, IViewFor<IngredientsPageViewModel>
	{
	    

	    public IngredientsPage ()
		{
			InitializeComponent ();

		    this.WhenActivated(closer =>
		    {
                ViewModel?.Activate();
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
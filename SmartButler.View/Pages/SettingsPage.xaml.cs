using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage, IViewFor<Logic.ViewModels.SettingsPageViewModel>
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

	    object IViewFor.ViewModel
	    {
	        get => ViewModel;
	        set => ViewModel = value as Logic.ViewModels.SettingsPageViewModel;
	    }

	    public Logic.ViewModels.SettingsPageViewModel ViewModel
	    {
	        get => BindingContext as Logic.ViewModels.SettingsPageViewModel;
	        set => BindingContext = value;
	    }
	}
}
using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage, IViewFor<SettingsPageViewModel>
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

	    object IViewFor.ViewModel
	    {
	        get => ViewModel;
	        set => ViewModel = value as SettingsPageViewModel;
	    }

	    public SettingsPageViewModel ViewModel
	    {
	        get => BindingContext as SettingsPageViewModel;
	        set => BindingContext = value;
	    }
	}
}
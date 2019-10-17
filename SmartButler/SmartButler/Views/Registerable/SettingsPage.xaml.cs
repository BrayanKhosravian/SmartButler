using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.Views.Registerable
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
using ReactiveUI;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartButler.View.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToolBarControl : ContentView, IViewFor<ToolbarControlViewModel>
	{
		public ToolBarControl ()
		{
			InitializeComponent ();
		}

		object IViewFor.ViewModel
	    {
	        get => ViewModel;
	        set => ViewModel = value as ToolbarControlViewModel;
	    }

	    public ToolbarControlViewModel ViewModel
	    {
	        get => BindingContext as ToolbarControlViewModel;
	        set => BindingContext = value;
	    }
	}
}
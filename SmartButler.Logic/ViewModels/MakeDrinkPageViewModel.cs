using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;

namespace SmartButler.Logic.ViewModels
{
	public class MakeDrinkPageViewModel : BaseViewModel
    {
        public MakeDrinkPageViewModel(INavigationService navigationService)
        { 

        }

        public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
        public void SetToolBarControlViewModel(ToolbarControlViewModel vm)
        {
            ToolbarControlViewModel = vm;
        }

    }
}

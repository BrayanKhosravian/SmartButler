using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.Services.Registrable;

namespace SmartButler.ViewModels
{
    class MakeDrinkPageViewModel : BaseViewModel
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

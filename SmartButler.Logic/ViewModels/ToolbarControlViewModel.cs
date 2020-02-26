using System.Windows.Input;
using ReactiveUI;
using SmartButler.Logic.Common;
using SmartButler.Logic.Interfaces;

namespace SmartButler.Logic.ViewModels
{
    public class ToolbarControlViewModel : ViewModelBase
    {

        public ToolbarControlViewModel(INavigationService navigationService)
        {
            SettingsCommand = ReactiveCommand.Create(async ()=> await navigationService.PushAsync<SettingsPageViewModel>());
        }

        public ReactiveCommand SettingsCommand { get; set; }

    }
}

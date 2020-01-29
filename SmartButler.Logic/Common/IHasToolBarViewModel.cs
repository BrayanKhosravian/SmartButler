using SmartButler.Logic.ViewModels;

namespace SmartButler.Logic.Common
{
	public interface IHasToolBarViewModel
	{
		ToolbarControlViewModel ToolbarControlViewModel { get; }
		void SetToolBarControlViewModel(ToolbarControlViewModel vm);
	}
}
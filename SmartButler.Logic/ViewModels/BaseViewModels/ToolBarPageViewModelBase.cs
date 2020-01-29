using System;
using System.Collections.Generic;
using System.Text;
using SmartButler.Logic.Common;

namespace SmartButler.Logic.ViewModels.BaseViewModels
{
	public abstract class ToolBarPageViewModelBase : ViewModelBase, IHasToolBarViewModel
	{
		public ToolbarControlViewModel ToolbarControlViewModel { get; private set; }
		public void SetToolBarControlViewModel(ToolbarControlViewModel vm) => ToolbarControlViewModel = vm;
	}
}

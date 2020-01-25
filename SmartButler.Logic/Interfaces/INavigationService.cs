using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using SmartButler.Logic.Common;

namespace SmartButler.Logic.Interfaces
{
	public interface INavigationService
	{
		Task PopAsync(bool animated = false);

		Task PopToRootAsync(bool animated = false);

		Task PopModalAsync(bool animated = false);

		Task PushAsync<TViewModel>(bool animated = false)
			where TViewModel : BaseViewModel;

		Task PushAsync<TViewModel>(Parameter parameter, bool animated = false)
			where TViewModel : BaseViewModel;

		Task PushAsync<TViewModel>(Parameter[] parameters, bool animated = false)
			where TViewModel : BaseViewModel;

		Task PushModalAsync<TViewModel>(bool animated = false)
			where TViewModel : BaseViewModel;

	}
}

using System.Threading.Tasks;
using SmartButler.Logic.Interfaces;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Common
{
	public class UserInteraction : IUserInteraction
	{
		public Task DisplayAlertAsync(string title, string message, string cancel)
		{
			return ((App)Application.Current).MainPage.DisplayAlert(title, message, cancel);
		}

		public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
		{
			return ((App)Application.Current).MainPage.DisplayAlert(title, message, accept, cancel);
		}

		public async Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
		{
			return await ((App)Application.Current).MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
		}

	}
}

using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SmartButler.Logic.Interfaces;
using Xamarin.Forms;

namespace SmartButler.Bootstrapper.Common
{
	public class UserInteraction : IUserInteraction
	{
		private static readonly IUserDialogs _dialogs = UserDialogs.Instance;

		public Task DisplayAlertAsync(string title, string message, string cancel)
		{
			return _dialogs.AlertAsync(message, title, cancel);
		}

		public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
		{
			return _dialogs.ConfirmAsync(title, message, accept, cancel);
		}

		public async Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
		{
			return await _dialogs.ActionSheetAsync(title, cancel, destruction, buttons: buttons);
		}

	}
}

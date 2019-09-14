using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartButler.Services.Registrable
{
    interface IUserInteraction
    {
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }

    class UserInteraction : IUserInteraction
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return ((App) Application.Current).MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return ((App)Application.Current).MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return ((App)Application.Current).MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

    }
}

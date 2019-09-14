using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartButler.Services
{
    public interface INavigationService
    {
        Task<Page> PopAsync(bool animated = false);

        Task<Page> PopModalAsync(bool animated = false);

        Task PushAsync<TView>(bool animated = false)
            where TView : Page;

        Task PushModalAsync<TView>(bool animated = false)
            where TView : Page;

    }
}
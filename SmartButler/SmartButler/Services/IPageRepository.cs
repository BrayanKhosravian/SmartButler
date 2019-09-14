using Autofac;
using Autofac.Core;
using SmartButler.ViewModels;
using Xamarin.Forms;

namespace SmartButler.Services
{
    /// <summary>
    ///This class saves the type relationship between a view and a viewmodel
    /// It saves them in a private dictionary where they key of the dictionary is the view
    ///
    /// Im using the ViewFirst-MVVM Pattern
    /// This means that the viewmodel is being instantiation first while other dependencies are registered
    /// Then the view is created and the views bindingcontext is set to the vm
    /// its not needed to pass in services into the view as u can pass them into the vm directly
    /// </summary>
    public interface IPageRepository
    {

        /// <summary>
        ///Registers the view and vm type
        /// Only 1 view is allowed to be registered as the view is the key in a Dictionary 
        /// This is because views are unique
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        void Register<TView, TViewModel>()
            where TView : Page
            where TViewModel : BaseViewModel;

        /// <summary>
        /// Resolve the view without using any parameters
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        Page Resolve<TView>()
            where TView : Page;

        /// <summary>
        /// Resolve the view using 1 parameter
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        Page ResolveWithParameter<TView>(NamedParameter parameter)
            where TView : Page;

        /// <summary>
        /// Resolve the view without using more parameters
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        Page ResolveWithParameters<TView>(params Parameter[] parameters)
            where TView : Page;

    }
}
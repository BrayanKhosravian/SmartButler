using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using SmartButler.Framework.Common;
using SmartButler.Logic.Common;
using SmartButler.Logic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartButler.Bootstrapper.Common
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
    public interface IPageRegistrar
    {

        /// <summary>
        ///Registers the view and vm type
        /// Only 1 view is allowed to be registered as the view is the key in a Dictionary 
        /// This is because views are unique
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        void Register<TViewModel, TView>()
	        where TViewModel : ViewModelBase
	        where TView : Page;

        /// <summary>
        /// Resolve the view without using any parameters
        /// </summary>
        Page Resolve<TViewModel>()
	        where TViewModel : ViewModelBase;

		/// <summary>
		/// Resolve the view using 1 parameter
		/// </summary>
		Page Resolve<TViewModel>(TypedParameter parameter)
            where TViewModel : ViewModelBase;

		/// <summary>
		/// Resolve the view without using more parameters
		/// </summary>
		Page Resolve<TViewModel>(params Parameter[] parameters)
            where TViewModel : ViewModelBase;
    }



    /// <inheritdoc cref="IPageRegistrar"/>/>
    public sealed class PageRegistrar : IPageRegistrar
    {
		// <TViewModel, TView>
		private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

		private readonly IComponentContext _componentContext;

        // inject autofac containerContext => is needed for resolving views & vm
        public PageRegistrar(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

       
        public void Register<TViewModel, TView>()
	        where TViewModel : ViewModelBase
	        where TView : Page
		{
            if(_map.ContainsKey(typeof(TView)))
               throw ExceptionFactory.Get<DuplicateViewRegisteredException>( "A duplicate view was already registered!" );
            _map[typeof(TViewModel)] = typeof(TView);
        }

        public Page Resolve<TViewModel>() 
	        where TViewModel : ViewModelBase
        {

            var vm = GetViewModel<TViewModel>();
            var page = GetPage<TViewModel>();

            page.BindingContext = vm;

            return page;
        }

        public Page Resolve<TViewModel>(TypedParameter parameter)
	        where TViewModel : ViewModelBase
        {

            var vm = GetViewModel<TViewModel>(parameter);
            var page = GetPage<TViewModel>();

            page.BindingContext = vm;

            return page;
        }

        public Page Resolve<TViewModel>(params Parameter[] parameters) 
	        where TViewModel : ViewModelBase
        {
            var vm = GetViewModel<TViewModel>(parameters);
            var page = GetPage<TViewModel>();

            page.BindingContext = vm;

            return page;
        }


        private Page GetPage<TViewModel>() where TViewModel : ViewModelBase
        {
	        var pageType = _map[typeof(TViewModel)];

	        var page = _componentContext.Resolve(pageType) as Page;
            return page;
        }

        private ViewModelBase GetViewModel<TViewModel>(params Parameter[] parameters)
        {
			var index = _map.IndexOf(kvp => kvp.Key == typeof(TViewModel));
			var vmType = _map.ElementAt(index).Key;

            if (parameters is null)
                throw ExceptionFactory.Get<ArgumentNullException>();

            ViewModelBase vm = null;

            if (parameters.Length == 0)     
                vm = (ViewModelBase) _componentContext.Resolve(vmType);
            if (parameters.Length == 1)
                vm = (ViewModelBase) _componentContext.Resolve(vmType, parameters[0]);
            if (parameters.Length > 1)
                vm = (ViewModelBase) _componentContext.Resolve(vmType, parameters);

            TrySetToolBarViewModel(vm);

            return vm;
        }

        private void TrySetToolBarViewModel(ViewModelBase vm)
        {
	        if (!(vm is IHasToolBarViewModel hasToolBarViewModel)) return;

	        var toolBar = _componentContext.Resolve<ToolbarControlViewModel>();
	        hasToolBarViewModel.SetToolBarControlViewModel(toolBar);
        }

    }

 
    public class DuplicateViewRegisteredException : Exception
    {
       
        public DuplicateViewRegisteredException()
        {
        }

        public DuplicateViewRegisteredException(string message) : base((string) message)
        {
        }

        public DuplicateViewRegisteredException(string message, Exception inner) : base((string) message, (Exception) inner)
        {
        }

    }


}

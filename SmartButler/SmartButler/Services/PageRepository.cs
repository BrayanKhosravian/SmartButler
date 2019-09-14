using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;
using Autofac;
using Autofac.Core;
using SmartButler.Bootstrapper;
using SmartButler.Core;
using SmartButler.Interfaces;
using SmartButler.ViewModels;
using SmartButler.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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



    /// <inheritdoc cref="IPageRepository"/>/>
    public class PageRepository : IPageRepository
    {
        // <TView, TViewModel>
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        private readonly IComponentContext _componentContext;

        // inject autofac containerContext // is needed for resolving views & vm
        public PageRepository(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

       
        public void Register<TView, TViewModel>() 
            where TView : Page 
            where TViewModel : BaseViewModel
        {
            if(_map.ContainsKey(typeof(TView)))
                ThrowDuplicateViewRegisteredException();
                
            _map[typeof(TView)] = typeof(TViewModel);
        }

        
        public Page Resolve<TView>() where TView : Page
        {

            var vm = GetViewModel<TView>();
            var view = GetPage<TView>();

            view.BindingContext = vm;

            return view;
        }

        public Page ResolveWithParameter<TView>(NamedParameter parameter) where TView : Page
        {
            
            var viewModel = _componentContext.Resolve<TView>(parameter);
            var view = this.GetPage<TView>();
            view.BindingContext = viewModel;

            return view;
        }

        public Page ResolveWithParameters<TView>(params Parameter[] parameters) where TView : Page
        {
            TView viewModel = _componentContext.Resolve<TView>(parameters);
            var view = this.GetPage<TView>();
            view.BindingContext = viewModel;

            return view;
        }


        private Page GetPage<TView>() where TView : Page
        {
            var index = _map.IndexOf(kvp => kvp.Key == typeof(TView));
            var viewType = _map.ElementAt(index).Key;

            Page page = _componentContext.Resolve(viewType) as Page;
            return page;
        }

        private BaseViewModel GetViewModel<TView>()
        {
            var vmType = _map[typeof(TView)];
            BaseViewModel vm = _componentContext.Resolve(vmType) as BaseViewModel;
            return vm;
        }

        private void ThrowDuplicateViewRegisteredException([CallerMemberName] string callerName = null)
        {
            throw new DuplicateViewRegisteredException($"A duplicate view was registered in: \n {this.GetType()} \n {callerName}");
        }

    }

 
    public class DuplicateViewRegisteredException : Exception
    {
       
        public DuplicateViewRegisteredException()
        {
        }

        public DuplicateViewRegisteredException(string message) : base(message)
        {
        }

        public DuplicateViewRegisteredException(string message, Exception inner) : base(message, inner)
        {
        }

    }

}

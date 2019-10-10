using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using SmartButler.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartButler.Services.Registrable
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
        void Register<TView, TViewModel>()
            where TView : Page
            where TViewModel : BaseViewModel;

        /// <summary>
        /// Resolve the view without using any parameters
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        TView Resolve<TView>()
            where TView : Page;

        /// <summary>
        /// Resolve the view using 1 parameter
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        TView ResolveWithParameter<TView>(NamedParameter parameter)
            where TView : Page;

        /// <summary>
        /// Resolve the view without using more parameters
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        TView ResolveWithParameters<TView>(params Parameter[] parameters)
            where TView : Page;

    }



    /// <inheritdoc cref="IPageRegistrar"/>/>
    public sealed class PageRegistrar : IPageRegistrar
    {
        // <TView, TViewModel>
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        private readonly IList<Type> _workspaceViewModels = new List<Type>();

        private readonly IComponentContext _componentContext;

        // inject autofac containerContext // is needed for resolving views & vm
        public PageRegistrar(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

       
        public void Register<TView, TViewModel>() 
            where TView : Page 
            where TViewModel : BaseViewModel
        {
            if(_map.ContainsKey(typeof(TView)))
               throw ExceptionFactory.Get<DuplicateViewRegisteredException>(new []{ "A duplicate view was already registered!" });
            _map[typeof(TView)] = typeof(TViewModel);
        }

        public TView Resolve<TView>() where TView : Page
        {

            var vm = GetViewModel<TView>();
            var view = GetPage<TView>();

            view.BindingContext = vm;

            return view;
        }

        public TView ResolveWithParameter<TView>(NamedParameter parameter) where TView : Page
        {

            var vm = GetViewModel<TView>(parameter);
            var view = GetPage<TView>();

            view.BindingContext = vm;

            return view;
        }

        public TView ResolveWithParameters<TView>(params Parameter[] parameters) where TView : Page
        {
            var vm = GetViewModel<TView>(parameters);
            var view = GetPage<TView>();

            view.BindingContext = vm;

            return view;
        }


        private TView GetPage<TView>() where TView : Page
        {
            var index = _map.IndexOf(kvp => kvp.Key == typeof(TView));
            var viewType = _map.ElementAt(index).Key;

            var page = _componentContext.Resolve(viewType) as TView;
            return page;
        }

        private BaseViewModel GetViewModel<TView>(params Parameter[] parameters)
        {
            Type vmType = _map[typeof(TView)];

            if (parameters is null)
                throw ExceptionFactory.Get<ArgumentNullException>();

            if (parameters.Length == 0)     
                return (BaseViewModel) _componentContext.Resolve(vmType);
            if (parameters.Length == 1)
                return (BaseViewModel) _componentContext.Resolve(vmType, parameters[0]);
            if (parameters.Length > 1)
                return (BaseViewModel) _componentContext.Resolve(vmType, parameters);

            throw ExceptionFactory.Get<ArgumentException>(parameters.Select(param => param.ToString()));

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

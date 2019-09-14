using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace SmartButler.Services
{
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
            _map[typeof(TView)] = typeof(TViewModel);
        }

        
        public Page Resolve<TView>() where TView : Page
        {

            var test = _map[typeof(TView)];


            var viewModel = _componentContext.Resolve<TView>();
            var view = GetPage<TView>();

            view.BindingContext = viewModel;

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
            Type viewType = _map[typeof(TView)];

            Page page = _componentContext.Resolve(viewType) as Page;

            return page;
        }

    }
}

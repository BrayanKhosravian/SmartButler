﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SmartButler.ViewModels;
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

    public class NavigationService : INavigationService
    {
        private readonly IPageRepository _pageRepository;

        public NavigationService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<Page> PopAsync(bool animated = false)
        {
            return await ((App)Application.Current).MainPage.Navigation.PopAsync(animated);
        }

        public async Task<Page> PopModalAsync(bool animated = false)
        {
            return await ((App)Application.Current).MainPage.Navigation.PopModalAsync(animated);
        }

        public async Task PushAsync<TView>(bool animated = false) where TView : Page
        {
            var page = _pageRepository.Resolve<TView>();

            await Task.FromResult(((App)Application.Current).MainPage.Navigation.PushAsync(page, animated));
        }

        public async Task PushModalAsync<TView>(bool animated = false) where TView : Page
        {
            var page = _pageRepository.Resolve<TView>();

            await Task.FromResult(((App)Application.Current).MainPage.Navigation.PushModalAsync(page, animated));
        }

    }
}

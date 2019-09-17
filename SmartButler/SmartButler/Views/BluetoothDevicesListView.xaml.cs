﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SmartButler.Core;
using SmartButler.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BindingDirection = ReactiveUI.BindingDirection;

namespace SmartButler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothDevicesListView : ContentPage, IViewFor<BluetoothDevicesViewModel>
    {

        public BluetoothDevicesListView()
        {
            InitializeComponent();

            this.WhenActivated(disposer =>
            {
                ViewModel?.ConfigureViewModel();

            });


        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var device = ((ListView) sender).SelectedItem as BluetoothDevice;
            await ViewModel.DeviceSelectedAsync(device.Mac, device.Name);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as BluetoothDevicesViewModel;
        }

        public BluetoothDevicesViewModel ViewModel
        {
            get => BindingContext as BluetoothDevicesViewModel;
            set => BindingContext = value;
        }
    }
}

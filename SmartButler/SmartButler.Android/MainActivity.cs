﻿using System;
using System.Collections;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SmartButler.Droid.Services;
using System.Collections.Generic;
using System.IO;
using Android.Bluetooth;
using Java.IO;
using Java.Lang;
using SmartButler.Interfaces;

namespace SmartButler.Droid
{
    [Activity(Label = "SmartButler", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var app = new App();

            IDictionary<Type, Type> types = new Dictionary<Type, Type>(); 
            types.Add(typeof(BluetoothService), typeof(IBluetoothService));

            app.InjectPlatformDependencies();

            LoadApplication(app);
        }
    }
}
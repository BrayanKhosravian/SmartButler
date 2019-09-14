﻿using System;
using System.Collections;
using System.Collections.Generic;
using SmartButler.Bootstrapper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmartButler
{
    public partial class App : Application
    {

        private Bootstrapper.Bootstrapper _bootstrapper;

        public App()
        {
            InitializeComponent();
        }

        // Entry point of PCL // call this from every project
        // pass in the dependencies as registered types
        public void InjectPlatformDependencies(IDictionary<Type, Type> types = null)
        {
            _bootstrapper.Load(this,types);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

   
}
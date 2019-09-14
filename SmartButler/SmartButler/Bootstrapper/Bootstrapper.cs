using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace SmartButler.Bootstrapper
{
    public class Bootstrapper
    {
        public void Load(App application, IDictionary<Type,Type> platformTypes = null)
        {
            var builder = new ContainerBuilder();

            // register types from other platforms to the IOC container
            if (platformTypes != null && platformTypes.Count > 0)
            {
               
            }

            // register views and viewmodels



            // register services



            // build container and load first page


            // application.MainPage = 
        }

    }
}

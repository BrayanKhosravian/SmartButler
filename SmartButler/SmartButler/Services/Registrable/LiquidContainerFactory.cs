using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SmartButler.Models;
using Xamarin.Forms;

namespace SmartButler.Services.Registrable
{

    /// <summary>
    /// This method creates either "Bottle.cs" or "Drink.cs"
    /// </summary>
    public interface ILiquidContainerFactory
    {
        /// <summary>
        /// This method creates either "Bottle.cs" or "Drink.cs"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name of the drink.</param>
        /// <param name="partialResource">The partial resource. Keep care to place the resources in "SmartButler.Resources"</param>
        /// <param name="resolvingType">The type where this resource is needed. (most of the time its a ViewModel)</param>
        /// <returns></returns>
        T Get<T>(string name, string partialResource, Type resolvingType)
            where T : LiquidContainer;
    }

    ///<inheritdoc cref="ILiquidContainerFactory"/>
    class LiquidContainerFactory : ILiquidContainerFactory
    {
        
        public T Get<T>(string name, string partialResource, Type resolvingType) 
            where T : LiquidContainer
        {
            var liquidContainer = Activator.CreateInstance<T>();
            var sourceAssembly = resolvingType.GetTypeInfo().Assembly;
            var resource = string.Join(".", "SmartButler.Resources", partialResource);
            liquidContainer.Name = name;

            liquidContainer.ActualImage = ImageSource.FromResource(resource, sourceAssembly);

            return liquidContainer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartButler.Models;
using Xamarin.Forms;

namespace SmartButler.Services.Registrable
{


	/// <summary>
	/// This method creates either "Ingredient.cs" or "DrinkRecipe.cs"
	/// </summary>
	[Obsolete]
	public interface ILiquidContainerFactory
    {
        /// <summary>
        /// This method creates either "Ingredient.cs" or "DrinkRecipe.cs"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name of the drink.</param>
        /// <param name="partialResource">The partial resource. Keep care to place the resources in "SmartButler.Resources"</param>
        /// <param name="resolvingType">The type where this resource is needed. (most of the time its a ViewModel)</param>
        /// <returns></returns>
        T Create<T>(string name, string partialResource, Type resolvingType)
            where T : LiquidBase;
    }


	///<inheritdoc cref="ILiquidContainerFactory"/>
	[Obsolete]
	class LiquidContainerFactory : ILiquidContainerFactory
    {
        
        public T Create<T>(string name, string partialResource, Type resolvingType) 
            where T : LiquidBase
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

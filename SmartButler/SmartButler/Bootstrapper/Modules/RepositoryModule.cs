using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SmartButler.DataAccess.Repositories;

namespace SmartButler.Bootstrapper.Modules
{
	class RepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			// Dont resolve this directly! 
			// its preferred to have a single instance of SQLiteAsyncConnection because its expensive to create
			// that means this class is alive for the whole app lifecycle
			// I used this class as a base class of the concrete Repositories but the repositories resolved didnt share the same state of the base
			builder.RegisterType<RepositoryComponent>().AsSelf().SingleInstance();

			builder.RegisterType<DrinkRecipesRepository>().As<IDrinkRecipesRepository>();
			builder.RegisterType<IngredientsRepository>().As<IIngredientsRepository>();
		}
	}
}

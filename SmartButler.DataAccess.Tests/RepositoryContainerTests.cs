using System.ComponentModel;
using Autofac;
using NUnit.Framework;
using Shouldly;
using SmartButler.DataAccess.Repositories;

namespace SmartButler.DataAccess.Tests
{
	class RepositoryContainerTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void RegisterTwoRepositories_EnsureThatDataInBaseClass_IsShared()
		{
			// arrange
			var builder = new ContainerBuilder();
			builder.RegisterType<RepositoryComponent>().AsSelf().SingleInstance();
			builder.RegisterType<IngredientsRepository>().As<IIngredientsRepository>();
			builder.RegisterType<DrinkRecipesRepository>().As<IDrinkRecipesRepository>();
			var container = builder.Build();
			var ingredientRepository = container.Resolve<IIngredientsRepository>();
			((IngredientsRepository) ingredientRepository).Component.IsTest = true;

			// act
			var drinkRecipesRepository = container.Resolve<IDrinkRecipesRepository>();

			// assert
			((DrinkRecipesRepository)drinkRecipesRepository).Component.IsTest.ShouldBe(true);

		}

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using Shouldly;
using SmartButler.DataAccess.Models;
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
			builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
			builder.RegisterType<BottlesRepository>().As<IBottlesRepository>();
			var container = builder.Build();
			var ingredientRepository = container.Resolve<IIngredientRepository>();
			((IngredientRepository)ingredientRepository).Component.IsTest = true;

			// act
			var bottlesRepository = container.Resolve<IBottlesRepository>();

			// assert
			((IngredientRepository)ingredientRepository).Component.IsTest.ShouldBe(true);

		}

	}
}

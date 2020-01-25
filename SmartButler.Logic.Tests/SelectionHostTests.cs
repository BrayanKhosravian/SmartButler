using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;
using SmartButler.DataAccess.Models;
using SmartButler.Logic.ModelViewModels;
using SmartButler.Logic.Services;

namespace SmartButler.Logic.Tests
{
	[TestFixture]
	public class SelectionHostTests
	{

		[Test]
		public void AfterCallingSelectionTwice_SelectionIsNullForTheSecondTime_WhenAutoResetIsTrue()
		{
			// Arrange
			var selectionHost = new DrinkIngredientSelectionHost();
			selectionHost.IsAutoReset = true;
			selectionHost.Selection = new DrinkIngredientViewModel(new Ingredient());
			var result1 = selectionHost.Selection;

			// Act
			var result2 = selectionHost.Selection;

			// Arrange
			result1.ShouldNotBeNull();
			result2.ShouldBeNull();
		}

		[Test]
		public void AfterCallingSelectionTwice_SelectionIsNullForTheSecondTime_WhenAutoResetIsTrue_UsingAutofac()
		{
			// Arrange
			var builder = new ContainerBuilder();
			builder.RegisterType<DrinkIngredientSelectionHost>()
				.As<ISelectionHost<DrinkIngredientViewModel>>()
				.SingleInstance();
			var container = builder.Build();
			var selectionHost = container.Resolve<ISelectionHost<DrinkIngredientViewModel>>();
			selectionHost.IsAutoReset = true;
			selectionHost.Selection = new DrinkIngredientViewModel(new Ingredient());
			var result1 = selectionHost.Selection;

			// Act
			var result2 = selectionHost.Selection;

			// Arrange
			result1.ShouldNotBeNull();
			result2.ShouldBeNull();
		}
	}
}

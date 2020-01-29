using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;
using SmartButler.Logic.ModelTemplates.Drinks;
using SmartButler.Logic.ModelViewModels;

namespace SmartButler.Logic.Tests
{
	[TestFixture()]
	public class ByteArrayEqualityComparerTests
	{
		[Test]
		public void GetHashCode_WhenValuesOfByteArraysAreTheSame_ShouldReturnTheSameHash()
		{
			var d1 = new Madras();
			var d2 = new Madras();
			var comparer = DrinkIngredientViewModel.ByteArrayEqualityComparer;

			var c1 = comparer.GetHashCode(d1.ByteImage);
			var c2 = comparer.GetHashCode(d2.ByteImage);

			c1.ShouldBe(c2);
		}
	}
}

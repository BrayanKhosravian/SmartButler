using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;

namespace SmartButler.DataAccess.Tests
{
    public class IngredientRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

		[Test]
		public async Task CallConfigureTwice_OneTable_And_NoDuplicateIngredients()
		{
			// arrange
			var ingredientRepository = new IngredientRepository(new RepositoryComponent());
			ingredientRepository.Component.IsTest = true;

			// act
			await ingredientRepository.ConfigureAsync(TestData.CreateDefaultIngredients());
			await ingredientRepository.ConfigureAsync(TestData.CreateDefaultIngredients());

			// assert
			var connection = ingredientRepository.Component.Connection;
			var tableCount = await ingredientRepository.Component.Connection.TableCount(IngredientRepository.TableName);
			var items = await connection.Table<Ingredient>().ToListAsync();

			tableCount.ShouldBe(1);
			items.Count.ShouldBe(6);

		}
	}

}
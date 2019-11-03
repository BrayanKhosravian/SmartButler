using System;
using System.Data;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartButler.Models;
using SmartButler.Repositories;
using SmartButler.Services.RegisterAble;
using SmartButler.Services.Registrable;
using SQLite;

namespace SmartButler.Tests
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
	        var ingredientRepository = new IngredientRepository(new IngredientFactory(new IngredientBuilder()));
	        ingredientRepository.IsTest = true;

			// act
	        await ingredientRepository.ConfigureAsync();
	        await ingredientRepository.ConfigureAsync();

			// assert
			var connection = ingredientRepository.Connection;
			var tableCount = await ingredientRepository.TableCount(IngredientRepository.TableName);
			var items = await connection.Table<Ingredient>().ToListAsync();

			tableCount.ShouldBe(1);
			items.Count.ShouldBe(6);

        }
    }

}
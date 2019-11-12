using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NUnit.Framework;
using ReactiveUI;
using SmartButler.DataAccess.Models;
using SmartButler.DataAccess.Repositories;
using SmartButler.Logic.Services;
using SQLite;

namespace Integration.Tests
{
	public class SqLiteDatabaseQueryTests
	{
		private RepositoryComponent _repositoryComponent;

		[SetUp]
		public async Task Setup()
		{
			_repositoryComponent = new RepositoryComponent();
			_repositoryComponent.IsTest = true;

			var ingredientsFactory = new IngredientsFactory(new IngredientBuilder());
			var ingredientsRepository = new IngredientsRepository(_repositoryComponent);
			await ingredientsRepository.ConfigureAsync(ingredientsFactory.GetDefaultIngredients());

			var drinkRecipesFactory = new DrinkRecipeFactory(new DrinkRecipeBuilder());
			var drinkRecipesRepository = new DrinkRecipesRepository(_repositoryComponent);
			await drinkRecipesRepository.ConfigureAsync(drinkRecipesFactory.GetDefaultDrinks());

		}

		//[Test]
		//public async Task Test1()
		//{
		//	var connection = _repositoryComponent.Connection;

		//	var drinks = await connection.Table<DrinkRecipe>().ToListAsync();

		//	var results = await QueryValuations(connection, drinks.First());

		//	Assert.Pass();
		//}

		//public async Task<IEnumerable<DrinkIngredient>> QueryValuations(SQLiteAsyncConnection db, DrinkRecipe drink)
		//{

		//	//return await db.QueryAsync(new TableMapping(typeof(DrinkRecipe)), drink.Id.ToString());
		//	//return await db.QueryAsync("select * from Valuation where StockId = ?", new object[]{drink.Id});
		//}
	}
}
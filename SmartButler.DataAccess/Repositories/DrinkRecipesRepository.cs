using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;

namespace SmartButler.DataAccess.Repositories
{
	public interface IDrinkRecipesRepository
	{
		Task ConfigureAsync(IEnumerable<DrinkRecipe> drinkRecipes);
		Task<bool> InsertWithChildrenAsync(DrinkRecipe ingredient);
		Task UpdateWithChildrenAsync(DrinkRecipe ingredient);
		Task UpsertWithChildrenAsync(DrinkRecipe ingredient);

		Task DeleteAsync(DrinkRecipe drinkRecipe);
		Task DeleteAsync(int id);

		Task<DrinkRecipe> GetAsync(int id);
		Task<List<DrinkRecipe>> GetAllAsync();
		Task<List<DrinkRecipe>> GetAllAvailableAsync();
	}

	public class DrinkRecipesRepository : IDrinkRecipesRepository
    {
	    public const string TableName = TableNames.DrinkRecipeTable;

	    public RepositoryComponent Component { get; }
		private readonly IDrinkIngredientRepository _drinkIngredientRepository;

		public DrinkRecipesRepository(RepositoryComponent repositoryComponent, 
			IDrinkIngredientRepository drinkIngredientRepository)
	    {
		    Component = repositoryComponent;
		    _drinkIngredientRepository = drinkIngredientRepository;
	    }

	    public async Task ConfigureAsync(IEnumerable<DrinkRecipe> drinks)
	    {
		    await Component.ConfigureAsync();

		    if (await Component.Connection.Table<DrinkRecipe>().CountAsync() > 0)
			    return;

		   await Component.Connection.InsertOrReplaceAllWithChildrenAsync(drinks, true);
	    }

	    public async Task<bool> InsertWithChildrenAsync(DrinkRecipe drink)
	    {
		    if (await IsInserted(drink)) return false;

			await Component.Connection.InsertOrReplaceWithChildrenAsync(drink, true);
			return true;
	    }

	    public Task UpdateWithChildrenAsync(DrinkRecipe drink)
	    {
		    return Component.Connection.UpdateWithChildrenAsync(drink);
		}

	    public async Task UpsertWithChildrenAsync(DrinkRecipe drink)
	    {
		    var drinkIngredients = drink.DrinkIngredients;
		    foreach (var di in drinkIngredients)
		    {
			    di.DrinkId = drink.Id;
		    }

		    var dBIngredients = await Component.Connection.Table<DrinkIngredient>()
			    .Where(di => di.DrinkId == drink.Id)
			    .ToListAsync();
		    
			var shouldBeDeleted = dBIngredients.
					Where(dbIngredient => drinkIngredients.All(di => di.IngredientId != dbIngredient.IngredientId))
					.ToList();

			await _drinkIngredientRepository.DeleteAsync(shouldBeDeleted);

			foreach (var drinkIngredient in drinkIngredients)
			{
				var doesExist = dBIngredients.Any(di => di.IngredientId == drinkIngredient.IngredientId &&
				                                        di.DrinkId == drinkIngredient.DrinkId);

				if (doesExist)
				    await _drinkIngredientRepository.UpdateAsync(drinkIngredient);
			    else
				    await _drinkIngredientRepository.InsertAsync(drinkIngredient);

		    }

			using var connection = new SQLiteConnection(Component.Connection.DatabasePath);
		    var command = new SQLiteCommand(connection);

			command.CommandText = $"update {TableNames.DrinkRecipeTable} set Name = @name, ByteImage = @byteImage " +
			                      $"where Id = '{drink.Id}'";
			command.Bind("@name", drink.Name);
			command.Bind("@byteImage", drink.ByteImage);
			command.ExecuteNonQuery();

	    }

	    public Task DeleteAsync(DrinkRecipe drink)
	    {
			return Component.Connection.DeleteAsync(drink);
		}

	    public Task DeleteAsync(int id)
	    {
			return Component.Connection.Table<DrinkRecipe>().DeleteAsync(d => d.Id == id);
		}

		public Task<DrinkRecipe> GetAsync(int id)
		{
			return Component.Connection.GetAsync<DrinkRecipe>(d => d.Id == id);
		}

		public Task<List<DrinkRecipe>> GetAllAsync()
		{
			return Component.Connection.GetAllWithChildrenAsync<DrinkRecipe>(recursive: true);
		}

		public Task<List<DrinkRecipe>> GetAllAvailableAsync()
		{
			throw new NotImplementedException();
			//return Component.Connection.Table<DrinkRecipe>()
			//	.Where(d => d.DrinkIngredients.All(i => i.Ingredient.IsAvailable)).ToListAsync();
		}

		private async Task<bool> IsInserted(DrinkRecipe drink)
	    {
		    var isInserted = await Component.Connection.Table<DrinkRecipe>().CountAsync(d => d.Name == drink.Name) > 0;

		    return isInserted;
	    }
    }
}

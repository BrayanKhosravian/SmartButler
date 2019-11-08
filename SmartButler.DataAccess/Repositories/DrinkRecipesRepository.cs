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

		Task DeleteAsync(DrinkRecipe ingredient);
		Task DeleteAsync(int id);

		Task<DrinkRecipe> GetAsync(int id);
		Task<List<DrinkRecipe>> GetAllAsync();
		Task<List<DrinkRecipe>> GetAllAvailableAsync();
	}

	public class DrinkRecipesRepository : IDrinkRecipesRepository
    {
	    public const string TableName = TableNames.DrinkRecipeTable;

		public RepositoryComponent Component { get; }

	    public DrinkRecipesRepository(RepositoryComponent repositoryComponent)
	    {
		    Component = repositoryComponent;
	    }

	    public async Task ConfigureAsync(IEnumerable<DrinkRecipe> drinks)
	    {
		    await Component.ConfigureAsync();

			foreach (var drink in drinks)
				await InsertWithChildrenAsync(drink);
	    }

	    public async Task<bool> InsertWithChildrenAsync(DrinkRecipe drink)
	    {
		    if (await IsInserted(drink)) return false;

			await Component.Connection.InsertWithChildrenAsync(drink);
			return true;
	    }

	    public Task UpdateWithChildrenAsync(DrinkRecipe drink)
	    {
			return Component.Connection.UpdateWithChildrenAsync(drink);
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
			return Component.Connection.GetAllWithChildrenAsync<DrinkRecipe>();
		}

		public Task<List<DrinkRecipe>> GetAllAvailableAsync()
		{
			return Component.Connection.Table<DrinkRecipe>()
				.Where(d => d.Ingredients.All(i => i.IsAvailable)).ToListAsync();
		}

		private async Task<bool> IsInserted(DrinkRecipe drink)
	    {
		    var isInserted = await Component.Connection.Table<DrinkRecipe>().CountAsync(d => d.Name == drink.Name) > 0;

		    return isInserted;
	    }
    }
}

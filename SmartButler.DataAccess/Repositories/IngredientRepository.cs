using System.Collections.Generic;
using System.Threading.Tasks;
using SmartButler.DataAccess.Models;

namespace SmartButler.DataAccess.Repositories
{
	public interface IIngredientRepository : IRepository
	{
		Task ConfigureAsync(IEnumerable<Ingredient> ingredients);
		Task InsertAsync(Ingredient ingredient);
		Task UpdateAsync(Ingredient ingredient);

		Task DeleteAsync(Ingredient ingredient);
		Task DeleteAsync(int id);

		Task<Ingredient> GetAsync(int id);
		Task<List<Ingredient>> GetAllAsync();
		Task<List<Ingredient>> GetAllAvailableAsync();


	}

	public class IngredientRepository : BaseRepository, IIngredientRepository
	{
		public const string TableName = "Ingredients";

		public async Task ConfigureAsync(IEnumerable<Ingredient> ingredients)
		{
			if (await TableCount(TableName) <= 0)
				Connection.CreateTableAsync<Ingredient>().Wait();

			foreach (var ingredient in ingredients)
			{
				if(await Connection.Table<Ingredient>().CountAsync(i => i.Name == ingredient.Name && i.Milliliter == ingredient.Milliliter) == 0)
					await Connection.InsertAsync(ingredient);
			}
		}

		public Task InsertAsync(Ingredient ingredient)
		{
			return Connection.InsertAsync(ingredient);
		}

		public Task UpdateAsync(Ingredient ingredient)
		{
			return Connection.UpdateAsync(ingredient);
		}

		public Task DeleteAsync(Ingredient ingredient)
		{
			return Connection.DeleteAsync(ingredient);
		}

		public Task DeleteAsync(int id)
		{
			return Connection.Table<Ingredient>().DeleteAsync(i => i.Id == id);
		}

		public Task<Ingredient> GetAsync(int id)
		{
			return Connection.GetAsync<Ingredient>(i => i.Id == id);
		}

		public Task<List<Ingredient>> GetAllAsync()
		{
			return Connection.Table<Ingredient>().ToListAsync();
		}

		public Task<List<Ingredient>> GetAllAvailableAsync()
		{
			return Connection.Table<Ingredient>().Where(i => i.IsAvailable).ToListAsync();
		}

		

	}
}

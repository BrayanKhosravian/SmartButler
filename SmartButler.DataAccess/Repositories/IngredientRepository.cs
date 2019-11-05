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

	public class IngredientRepository : IIngredientRepository
	{
		public const string TableName = "Ingredients";

		public RepositoryComponent Component { get; }

		public IngredientRepository(RepositoryComponent component)
		{
			Component = component;
		}

		public async Task ConfigureAsync(IEnumerable<Ingredient> ingredients)
		{
			if (await Component.Connection.TableCount(TableName) <= 0)
				Component.Connection.CreateTableAsync<Ingredient>().Wait();

			foreach (var ingredient in ingredients)
			{
				if(await Component.Connection.Table<Ingredient>().CountAsync(i => i.Name == ingredient.Name && i.Milliliter == ingredient.Milliliter) == 0)
					await Component.Connection.InsertAsync(ingredient);
			}
		}

		public Task InsertAsync(Ingredient ingredient)
		{
			return Component.Connection.InsertAsync(ingredient);
		}

		public Task UpdateAsync(Ingredient ingredient)
		{
			return Component.Connection.UpdateAsync(ingredient);
		}

		public Task DeleteAsync(Ingredient ingredient)
		{
			return Component.Connection.DeleteAsync(ingredient);
		}

		public Task DeleteAsync(int id)
		{
			return Component.Connection.Table<Ingredient>().DeleteAsync(i => i.Id == id);
		}

		public Task<Ingredient> GetAsync(int id)
		{
			return Component.Connection.GetAsync<Ingredient>(i => i.Id == id);
		}

		public Task<List<Ingredient>> GetAllAsync()
		{
			return Component.Connection.Table<Ingredient>().ToListAsync();
		}

		public Task<List<Ingredient>> GetAllAvailableAsync()
		{
			return Component.Connection.Table<Ingredient>().Where(i => i.IsAvailable).ToListAsync();
		}

		

	}
}

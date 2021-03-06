﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SQLiteNetExtensionsAsync.Extensions;

namespace SmartButler.DataAccess.Repositories
{
	public interface IIngredientsRepository //: IRepository
	{
		Task ConfigureAsync(IEnumerable<Ingredient> ingredients);
		Task<bool> InsertAsync(Ingredient ingredient);
		Task UpdateAsync(Ingredient ingredient);
		Task DeleteAsync(Ingredient ingredient);
		Task DeleteAsync(int id);
		Task<Ingredient> GetAsync(int id);
		Task<List<Ingredient>> GetAllAsync();
		Task<List<Ingredient>> GetAllAvailableAsync();
	}

	public class IngredientsRepository : IIngredientsRepository
	{
		public const string TableName = TableNames.IngredientsTable;

		internal readonly RepositoryComponent Component;

		public IngredientsRepository(RepositoryComponent component)
		{
			Component = component;
		}


		public async Task ConfigureAsync(IEnumerable<Ingredient> ingredients)
		{
			await Component.ConfigureAsync();

			if (await Component.Connection.Table<Ingredient>().CountAsync() > 0)
				return;

			if (ingredients == null)
				return;

			await Component.Connection.InsertOrReplaceAllWithChildrenAsync(ingredients, true);

			//foreach (var ingredient in ingredients)
			//	await InsertAsync(ingredient);
		}

		public async Task<bool> InsertAsync(Ingredient ingredient)
		{
			if (await IsInserted(ingredient)) return false;

			await Component.Connection.InsertAsync(ingredient);
			await UpdateAsync(ingredient); // if any other is also selected, it will get unselected with this call
			return true;
		}

		public async Task UpdateAsync(Ingredient ingredient)
		{
			// if another Ingredient has same SelectedIndex => then set it to 0 (unselected)
			var shouldBeUnselected = await Component.Connection.Table<Ingredient>()
				.Where(i => i.BottleIndex != 0 && i.BottleIndex == ingredient.BottleIndex)
				.ToListAsync();

			if (shouldBeUnselected.Count > 0)
			{
				foreach (var unselect in shouldBeUnselected)
					unselect.BottleIndex = 0;
			}

			await Component.Connection.UpdateAllAsync(shouldBeUnselected);

			// update ingredient
			await Component.Connection.UpdateAsync(ingredient);

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
			throw new NotImplementedException();
			// return Component.Connection.Table<Ingredient>().Where(i => i.IsAvailable).ToListAsync();
		}

		private async Task<bool> IsInserted(Ingredient ingredient)
		{
			var isInserted = await Component.Connection.Table<Ingredient>().CountAsync(i =>
				                 i.Name == ingredient.Name) > 0;

			return isInserted;
		}

	}
}

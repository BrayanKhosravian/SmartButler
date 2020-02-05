using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SQLiteNetExtensionsAsync.Extensions;

namespace SmartButler.DataAccess.Repositories
{

	public interface IDrinkIngredientRepository
	{
		Task UpdateAsync(DrinkIngredient drinkIngredient);
		Task InsertAsync(DrinkIngredient drinkIngredient);
		Task DeleteAsync(DrinkIngredient drinkIngredient);
		Task DeleteAsync(List<DrinkIngredient> drinkIngredients);
	}

	public class DrinkIngredientRepository : IDrinkIngredientRepository
	{
		public const string TableName = TableNames.DrinkIngredientsTable;

		public RepositoryComponent Component { get; }

		public DrinkIngredientRepository(RepositoryComponent component)
		{
			Component = component;
		}

		public Task UpdateAsync(DrinkIngredient drinkIngredient)
		{
			return Component.Connection.UpdateAsync(drinkIngredient);
		}

		public Task InsertAsync(DrinkIngredient drinkIngredient)
		{
			return Component.Connection.InsertOrReplaceWithChildrenAsync(drinkIngredient, true);
		}

		public Task DeleteAsync(DrinkIngredient drinkIngredient)
		{
			return Component.Connection.DeleteAsync(drinkIngredient, recursive: false);
		}

		public Task DeleteAsync(List<DrinkIngredient> drinkIngredients)
		{
			return Component.Connection.DeleteAllAsync(drinkIngredients);
		}
	}
}

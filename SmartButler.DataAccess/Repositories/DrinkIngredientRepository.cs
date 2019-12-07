using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;

namespace SmartButler.DataAccess.Repositories
{

	public interface IDrinkIngredientRepository
	{
		Task UpdateAsync(DrinkIngredient drinkIngredient);
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

	}
}

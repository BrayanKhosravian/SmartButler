using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SmartButler.DataAccess.Common;
using SmartButler.DataAccess.Models;
using SQLite;

namespace SmartButler.DataAccess.Repositories
{
	/// <summary>
	/// Dont resolve this directly! 
	/// its preferred to have a single instance of SQLiteAsyncConnection because its expensive to create
	/// that means this class is alive for the whole app lifecycle
	/// I used this class as a base class of the concrete Repositories but the repositories resolved didnt share the same state of the base
	/// </summary>
	public class RepositoryComponent
	{
		private SQLiteAsyncConnection _connection;
	    private readonly string _documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		internal bool IsTest { get; set; }
		internal string Path => IsTest ? ":memory:" : System.IO.Path.Combine(_documentsPath, "SmartButler.db3");

		internal SQLiteAsyncConnection Connection
        {
			get
			{
				if (_connection != null) return _connection;

				_connection = new SQLiteAsyncConnection(Path);
				return _connection;
			}
        }

		internal async Task ConfigureAsync()
		{
#if DEBUG && false
			await Connection.DropTableAsync<Ingredient>();
			await Connection.DropTableAsync<DrinkIngredient>();
			await Connection.DropTableAsync<DrinkRecipe>();

#endif
			await ConfigureTableAsync<Ingredient>(TableNames.IngredientsTable);
			await ConfigureTableAsync<DrinkRecipe>(TableNames.DrinkRecipeTable);
			await ConfigureTableAsync<DrinkIngredient>(TableNames.DrinkIngredientsTable);

		}

		private async Task ConfigureTableAsync<TTable>(string tableName) where TTable : class, new()
		{
			if (await Connection.TableCount(tableName) <= 0)
				await Connection.CreateTableAsync<TTable>();
		}

	}
}

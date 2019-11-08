using System.Data;
using System.Threading.Tasks;
using SQLite;

namespace SmartButler.DataAccess.Common
{
	internal static class RepositoryExtensions
	{
		internal static async Task<int> TableCount(this SQLiteAsyncConnection connection, string tableName)
		{
			TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
			object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

			int tableCount = (await connection.QueryAsync(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'", ps))
				.Count; // Executes the query from which we can count the results
			return tableCount;

		}
	}
}

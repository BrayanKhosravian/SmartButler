using System;
using System.Data;
using System.Threading.Tasks;
using SQLite;

namespace SmartButler.DataAccess.Repositories
{
    public class BaseRepository
	{
		private SQLiteAsyncConnection _connection;
	    private readonly string _documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

		public bool IsTest { get; set; }
		public string Path => IsTest ? ":memory:" : System.IO.Path.Combine(_documentsPath, "SmartButler.db3");

		public SQLiteAsyncConnection Connection
        {
			get
			{
				if (_connection != null) return _connection;

				_connection = new SQLiteAsyncConnection(Path);
				return _connection;
			}
        }

		public async Task<int> TableCount(string tableName)
		{
			TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
			object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

			int tableCount =  (await Connection.QueryAsync(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'", ps))
				.Count; // Executes the query from which we can count the results
			return tableCount;

		}

	}
}

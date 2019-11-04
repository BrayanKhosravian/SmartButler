using System;
using System.Threading.Tasks;
using SQLite;

namespace SmartButler.DataAccess.Repositories
{
    class BottlesRepository : BaseRepository, IRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public BottlesRepository()
        {
            _connection = Connection;
        }

        public Task ConfigureAsync()
        {
	        throw new NotImplementedException();
        }
    }
}

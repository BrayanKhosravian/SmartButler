using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartButler.Interfaces;
using SmartButler.Models;
using SQLite;

namespace SmartButler.Services.Registrable
{
    class BottlesRepository : BaseRepository, IRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public BottlesRepository()
        {
            _connection = GetConnection();
        }

        public Task Configure()
        {
            return _connection.CreateTableAsync<Ingredient>();
        }

    }
}

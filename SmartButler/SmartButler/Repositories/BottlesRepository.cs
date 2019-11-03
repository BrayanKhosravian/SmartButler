using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartButler.Interfaces;
using SmartButler.Models;
using SmartButler.Repositories;
using SmartButler.Services.RegisterAble;
using SQLite;

namespace SmartButler.Services.Registrable
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

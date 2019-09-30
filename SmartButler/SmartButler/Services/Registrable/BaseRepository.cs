using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace SmartButler.Services.Registrable
{
    internal abstract class BaseRepository
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "SmartButler.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}

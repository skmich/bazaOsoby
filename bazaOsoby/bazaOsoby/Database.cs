using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SQLite;
using Xamarin.Forms;

namespace bazaOsoby
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Person>().Wait();
        }

        public Task<int> InsertPersonAsync(Person person)
        {
            return _connection.InsertAsync(person);
        }
        public Task<int> UpdatePersonAsync(Person person)
        {
            return _connection.UpdateAsync(person);
        }
        public Task<int> DeletePersonAsync(Person person)
        {
            return _connection.DeleteAsync(person);
        }
        public Task<List<Person>> GetAllPersonsAsync()
        {
            return _connection.Table<Person>().ToListAsync();
        }
    }
}

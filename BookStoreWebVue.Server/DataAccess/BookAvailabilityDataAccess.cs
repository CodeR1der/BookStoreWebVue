using BookStoreWebVue.Server.BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWebVue.Server.DataAccess
{
    public class BookAvailabilityDataAccess
    {
        private readonly string _connectionString;

        public BookAvailabilityDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BookAvailability> GetBookAvailabilities() 
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allBookAvailabilaties = db.GetTable<BookAvailability>()
                    .LoadWith(req => req.book)
                    .LoadWith(req => req.warehouse)
                    .LoadWith(req => req.warehouse.address)
                    .LoadWith(req => req.warehouse.address.country)
                    .LoadWith(req => req.book.genre)
                    .LoadWith(req => req.book.publisher)
                    .LoadWith(req => req.book.author)
                    .LoadWith(req => req.book.language)
                                 .ToList();
                return allBookAvailabilaties;
            }
        }
        public void PrintAllBookAvailability()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allAvailabilities = db.GetTable<BookAvailability>().LoadWith(request => request.book).LoadWith(request => request.warehouse).ToList();

                Console.WriteLine("{0,-10} {1,-20} {2, -30} {3, -40}" , "availability ID", "BookName", "Warehouse name", "quantity");
                Console.WriteLine(new string('-', 30));

                foreach (var availability in allAvailabilities)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2, -30} {3, -40}", availability.availabilityId , availability.book.title, availability.warehouse.warehouseName, availability.quantity);
                }
            }
        }
        public BookAvailability GetBookAvailabilityById(Guid bookAvailabilityId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<BookAvailability>().FirstOrDefault(p => p.availabilityId == bookAvailabilityId);
            }
        }

        public void AddBookAvailability(BookAvailability bookAvailability)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(bookAvailability);
            }
        }

        public void UpdateBookAvailability(BookAvailability bookAvailability)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(bookAvailability);
            }
        }

        public void DeleteBookAvailability(Guid bookAvailabilityId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<BookAvailability>().Delete(p => p.availabilityId == bookAvailabilityId);
            }
        }
    }
}

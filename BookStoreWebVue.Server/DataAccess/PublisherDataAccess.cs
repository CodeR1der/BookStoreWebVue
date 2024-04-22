using BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOperations
{
    public class PublisherDataAccess
    {
        private readonly string _connectionString;

        public PublisherDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Publisher> GetPublishers()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allPublishers = db.GetTable<Publisher>()
                                 .ToList();
                return allPublishers;
            }
        }
        public void PrintAllPublishers()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allPublishers = db.GetTable<Publisher>().ToList();

                Console.WriteLine("{0,-10} {1,-20}", "Publisher ID", "Publisher Name");
                Console.WriteLine(new string('-', 30));

                foreach (var publisher in allPublishers)
                {
                    Console.WriteLine("{0,-10} {1,-20}", publisher.publisherId, publisher.publisherName);
                }
            }
        }

        public void AddPublisher(Publisher publisher)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(publisher);
            }
        }

        public Publisher GetPublisherById(int publisherId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Publisher>().FirstOrDefault(p => p.publisherId == publisherId);
            }
        }

        public void UpdatePublisher(Publisher publisher)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(publisher);
            }
        }

        public void DeletePublisher(int publisherId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Publisher>().Delete(p => p.publisherId == publisherId);
            }
        }
    }

}

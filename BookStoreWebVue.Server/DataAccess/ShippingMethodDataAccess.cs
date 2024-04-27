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
    public class ShippingMethodDataAccess
    {
        private readonly string _connectionString;

        public ShippingMethodDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ShippingMethod> GetShippingMethods()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allShippingMethods = db.GetTable<ShippingMethod>()
                                 .ToList();
                return allShippingMethods;
            }
        }

        public void PrintAllShippingMethods()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allShippingMethods = db.GetTable<ShippingMethod>().ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-10}", "Method ID", "Method Name", "Cost");
                Console.WriteLine(new string('-', 50));

                foreach (var shippingMethod in allShippingMethods)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-10}", shippingMethod.methodId, shippingMethod.methodName, shippingMethod.cost);
                }
            }
        }

        public void AddShippingMethod(ShippingMethod shippingMethod)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(shippingMethod);
            }
        }

        public ShippingMethod GetShippingMethodById(Guid methodId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<ShippingMethod>().FirstOrDefault(s => s.methodId == methodId);
            }
        }

        public void UpdateShippingMethod(ShippingMethod shippingMethod)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(shippingMethod);
            }
        }
        public void DeleteShippingMethod(Guid shippingMethodId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<ShippingMethod>()
                    .Delete(co => co.methodId == shippingMethodId);
            }
        }
    }
}
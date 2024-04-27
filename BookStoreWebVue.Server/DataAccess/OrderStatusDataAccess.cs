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
    public class OrderStatusDataAccess
    {
        private readonly string _connectionString;

        public OrderStatusDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OrderStatus> GetOrderStatuses()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allOrderStatuses = db.GetTable<OrderStatus>()
                                 .ToList();
                return allOrderStatuses;
            }
        }

        public void PrintAllOrderStatuses()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allOrderStatuses = db.GetTable<OrderStatus>().ToList();

                Console.WriteLine("{0,-10} {1,-20}", "Status ID", "Status Value");
                Console.WriteLine(new string('-', 30));

                foreach (var orderStatus in allOrderStatuses)
                {
                    Console.WriteLine("{0,-10} {1,-20}", orderStatus.statusId, orderStatus.statusValue);
                }
            }
        }

        public void AddOrderStatus(OrderStatus orderStatus)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(orderStatus);
            }
        }

        public OrderStatus GetOrderStatusById(Guid statusId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<OrderStatus>().FirstOrDefault(o => o.statusId == statusId);
            }
        }

        public void UpdateOrderStatus(OrderStatus orderStatus)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(orderStatus);
            }
        }

        public void DeleteOrderStatus(Guid statusId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<OrderStatus>().Delete(o => o.statusId == statusId);
            }
        }
    }

}

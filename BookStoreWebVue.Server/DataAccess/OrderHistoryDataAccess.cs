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
    public class OrderHistoryDataAccess
    {
        private readonly string _connectionString;

        public OrderHistoryDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OrderHistory> GetOrderHistories()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allOrderHistories = db.GetTable<OrderHistory>()
                    .LoadWith(req=>req.order)
                    .LoadWith(req => req.status)
                                 .ToList();
                return allOrderHistories;
            }
        }

        public void PrintAllOrderHistories()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allOrderHistories = db.GetTable<OrderHistory>()
                    .LoadWith(oh => oh.order)
                    .LoadWith(oh => oh.status)
                    .ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}", "History ID", "Order ID", "Status", "Status Date");
                Console.WriteLine(new string('-', 70));

                foreach (var orderHistory in allOrderHistories)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20}", orderHistory.historyId, orderHistory.order.orderId, orderHistory.status.statusValue, orderHistory.statusDate);
                }
            }
        }

        public OrderHistory GetOrderHistoryById(int historyId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<OrderHistory>()
                    .FirstOrDefault(oh => oh.historyId == historyId);
            }
        }

        public void AddOrderHistory(OrderHistory orderHistory)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(orderHistory);
            }
        }

        public void UpdateOrderHistory(OrderHistory orderHistory)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(orderHistory);
            }
        }

        public void DeleteOrderHistory(int historyId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<OrderHistory>()
                    .Delete(oh => oh.historyId == historyId);
            }
        }
    }

}

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

        public OrderHistory GetOrderHistoryById(Guid historyId)
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

        public void DeleteOrderHistory(Guid historyId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<OrderHistory>()
                    .Delete(oh => oh.historyId == historyId);
            }
        }
    }

}

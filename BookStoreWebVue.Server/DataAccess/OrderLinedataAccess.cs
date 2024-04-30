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
    public class OrderLineDataAccess
    {
        private readonly string _connectionString;

        public OrderLineDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OrderLine> GetOrderLines()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allOrderLines = db.GetTable<OrderLine>()
                    .LoadWith(req => req.customerOrder)
                    .LoadWith(req => req.book)
                                 .ToList();
                return allOrderLines;
            }
        }
        public OrderLine GetOrderLineById(Guid lineId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<OrderLine>()
                    .FirstOrDefault(ol => ol.lineID == lineId);
            }
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(orderLine);
            }
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(orderLine);
            }
        }

        public void DeleteOrderLine(Guid lineId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<OrderLine>()
                    .Delete(ol => ol.lineID == lineId);
            }
        }
    }

}

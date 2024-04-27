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
        public void PrintAllOrderLines()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allOrderLines = db.GetTable<OrderLine>()
                    .LoadWith(ol => ol.customerOrder)
                    .LoadWith(ol => ol.book)
                    .ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-10} {5,-10}", "Line ID", "Order ID", "Book Title", "Price", "Quantity", "Total");
                Console.WriteLine(new string('-', 90));

                foreach (var orderLine in allOrderLines)
                {
                    double total = orderLine.price * orderLine.quantity;
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-10} {5,-10}", orderLine.lineID, orderLine.customerOrder.orderId, orderLine.book.title, orderLine.price, orderLine.quantity, total);
                }
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

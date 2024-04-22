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
    public class CustomerOrderDataAccess
    {
        private readonly string _connectionString;

        public CustomerOrderDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CustomerOrder> GetCustomerOrders()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCustomerOrders = db.GetTable<CustomerOrder>()
                    .LoadWith(req=>req.customer)
                    .LoadWith(req=> req.shippingMethod)
                    .LoadWith(req=> req.destAddress)
                                 .ToList();
                return allCustomerOrders;
            }
        }

        public void PrintAllCustomerOrders()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCustomerOrders = db.GetTable<CustomerOrder>()
                    .LoadWith(co => co.customer)
                    .LoadWith(co => co.shippingMethod)
                    .LoadWith(co => co.destAddress)
                    .ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-30}", "Order ID", "Order Date", "Customer Name", "Shipping Method", "Destination Address");
                Console.WriteLine(new string('-', 100));

                foreach (var customerOrder in allCustomerOrders)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-20} {4,-30}", customerOrder.orderId, customerOrder.orderDate, customerOrder.customer.firstName + " " + customerOrder.customer.lastName, customerOrder.shippingMethod.methodName, $"{customerOrder.destAddress.streetName}, {customerOrder.destAddress.city}, {customerOrder.destAddress.country}");
                }
            }
        }

        public CustomerOrder GetCustomerOrderById(int orderId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<CustomerOrder>()
                    .FirstOrDefault(co => co.orderId == orderId);
            }
        }

        public void AddCustomerOrder(CustomerOrder customerOrder)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(customerOrder);
            }
        }

        public void UpdateCustomerOrder(CustomerOrder customerOrder)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(customerOrder);
            }
        }

        public void DeleteCustomerOrder(int orderId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<CustomerOrder>()
                    .Delete(co => co.orderId == orderId);
            }
        }
    }

}

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
    public class CustomerDataAccess
    {
        private readonly string _connectionString;

        public CustomerDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetCustomers() 
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCustomers = db.GetTable<Customer>()
                                    .ToList();
                return allCustomers;
            }
        }
        public void PrintAllCustomers()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCustomers = db.GetTable<Customer>().ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-30}", "Customer ID", "First Name", "Last Name", "Email");
                Console.WriteLine(new string('-', 80));

                foreach (var customer in allCustomers)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-30}", customer.customerId, customer.firstName, customer.lastName, customer.email);
                }
            }
        }

        public void AddCustomer(Customer customer)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(customer);
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Customer>().FirstOrDefault(c => c.customerId == customerId);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(customer);
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Customer>().Delete(c => c.customerId == customerId);
            }
        }
    }

}

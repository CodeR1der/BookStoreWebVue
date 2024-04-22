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
    public class CustomerAddressDataAccess
    {
        private readonly string _connectionString;

        public CustomerAddressDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CustomerAddress> GetCustomerAddresses() 
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCustomerAddresses = db.GetTable<CustomerAddress>()
                    .LoadWith(req=> req.customer)
                    .LoadWith(req => req.address)
                                 .ToList();
                return allCustomerAddresses;
            }
        }

        public void PrintAllCustomerAddresses()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allCustomerAddresses = db.GetTable<CustomerAddress>()
                    .LoadWith(ca => ca.customer)
                    .LoadWith(ca => ca.address)
                    .ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-30} {3,-40}", "Customer ID", "Customer Name", "Street Name", "City");
                Console.WriteLine(new string('-', 60));

                foreach (var customerAddress in allCustomerAddresses)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-30} {3,-40}", customerAddress.customer.customerId, customerAddress.customer.firstName + " " + customerAddress.customer.lastName, customerAddress.address.streetName, customerAddress.address.city);
                }
            }
        }

        public CustomerAddress GetCustomerAddressById(int customerId, int addressId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<CustomerAddress>()
                    .FirstOrDefault(ca => ca.customerId == customerId && ca.addressId == addressId);
            }
        }

        public void AddCustomerAddress(CustomerAddress customerAddress)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(customerAddress);
            }
        }

        public void UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(customerAddress);
            }
        }

        public void DeleteCustomerAddress(int customerId, int addressId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<CustomerAddress>()
                    .Delete(ca => ca.customerId == customerId && ca.addressId == addressId);
            }
        }
    }

}

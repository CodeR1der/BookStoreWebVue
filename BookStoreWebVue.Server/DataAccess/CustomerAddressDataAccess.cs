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

        public CustomerAddress GetCustomerAddressById(Guid customerId, Guid addressId)
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

        public void DeleteCustomerAddress(Guid customerId, Guid addressId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<CustomerAddress>()
                    .Delete(ca => ca.customerId == customerId && ca.addressId == addressId);
            }
        }
    }

}

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
    public class SupplierDataAccess
    {
        private readonly string _connectionString;

        public SupplierDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Supplier> GetSuppliers()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allSuppliers = db.GetTable<Supplier>()
                                 .ToList();
                return allSuppliers;
            }
        }

        public void PrintAllSuppliers()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allSuppliers = db.GetTable<Supplier>().ToList();

                Console.WriteLine("{0,-10} {1,-20}", "Supplier ID", "Supplier Name");
                Console.WriteLine(new string('-', 30));

                foreach (var supplier in allSuppliers)
                {
                    Console.WriteLine("{0,-10} {1,-20}", supplier.supplierId, supplier.supplierName);
                }
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(supplier);
            }
        }

        public Supplier GetSupplierById(Guid supplierId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Supplier>().FirstOrDefault(s => s.supplierId == supplierId);
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(supplier);
            }
        }

        public void DeleteSupplier(Guid supplierId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Supplier>().Delete(s => s.supplierId == supplierId);
            }
        }
    }

}

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
    public class SupplyDataAccess
    {
        private readonly string _connectionString;

        public SupplyDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Supply> GetSupplies()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allSupplies = db.GetTable<Supply>()
                    .LoadWith(req => req.warehouse)
                    .LoadWith(req => req.supplier)
                    .ToList();
                return allSupplies;
            }
        }

        public void PrintAllSupplies()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allSupplies = db.GetTable<Supply>()
                    .LoadWith(s => s.supplier)
                    .LoadWith(s => s.warehouse)
                    .ToList();

                Console.WriteLine("{0,-10} {1,-20} {2,-20}", "Supply ID", "Supplier", "Warehouse");
                Console.WriteLine(new string('-', 50));

                foreach (var supply in allSupplies)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-20}", supply.supplyId, supply.supplier.supplierName, supply.warehouse.warehouseName);
                }
            }
        }

        public Supply GetSupplyById(Guid supplyId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Supply>()
                    .FirstOrDefault(s => s.supplyId == supplyId);
            }
        }

        public void AddSupply(Supply supply)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(supply);
            }
        }

        public void UpdateSupply(Supply supply)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(supply);
            }
        }

        public void DeleteSupply(Guid supplyId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Supply>()
                    .Delete(s => s.supplyId == supplyId);
            }
        }
    }

}

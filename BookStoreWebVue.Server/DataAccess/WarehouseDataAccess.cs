using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;


namespace TestOperations
{
    public class WarehouseDataAccess
    {
        private readonly string _connectionString;

        public WarehouseDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Warehouse> GetWarehouses()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allWarehouses = db.GetTable<Warehouse>()
                    .LoadWith(req => req.address)
                    .LoadWith(req=>req.address.country)
                                 .ToList();
                return allWarehouses;
            }
        }

        public void PrintAllWarehouses()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allWarehouses = db.GetTable<Warehouse>()
                    .LoadWith(w => w.address)
                    .ToList();

                Console.WriteLine("{0,-15} {1,-25} {2,-30}", "Warehouse ID", "Warehouse Name", "Address");
                Console.WriteLine(new string('-', 70));

                foreach (var warehouse in allWarehouses)
                {
                    Console.WriteLine("{0,-15} {1,-25} {2,-30}", warehouse.warehouseId, warehouse.warehouseName, $"{warehouse.address.streetName}, {warehouse.address.city}, {warehouse.address.country}");
                }
            }
        }

        public Warehouse GetWarehouseById(int warehouseId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<Warehouse>()
                    .FirstOrDefault(w => w.warehouseId == warehouseId);
            }
        }

        public void AddWarehouse(Warehouse warehouse)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(warehouse);
            }
        }

        public void UpdateWarehouse(Warehouse warehouse)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(warehouse);
            }
        }

        public void DeleteWarehouse(int warehouseId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<Warehouse>()
                    .Delete(w => w.warehouseId == warehouseId);
            }
        }
    }

}

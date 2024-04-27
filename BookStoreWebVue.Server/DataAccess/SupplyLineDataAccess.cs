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
    public class SupplyLineDataAccess
    {
        private readonly string _connectionString;

        public SupplyLineDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<SupplyLine> GetSupplyLines()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allSupplyLines = db.GetTable<SupplyLine>()
                    .LoadWith(req => req.supply)
                    .LoadWith(req => req.book)
                    .LoadWith(req => req.book.author)
                    .LoadWith(req => req.book.genre)
                    .LoadWith(req => req.book.publisher)
                    .LoadWith(req => req.book.language)
                    .LoadWith(req => req.supply.supplier)
                    .LoadWith(req => req.supply.warehouse)
                    .LoadWith(req => req.supply.warehouse.address)
                    .LoadWith(req => req.supply.warehouse.address.country)
                                 .ToList();
                return allSupplyLines;
            }
        }

        public void PrintAllSupplyLines()
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                var allSupplyLines = db.GetTable<SupplyLine>()
                    .LoadWith(sl => sl.supply)
                    .LoadWith(sl => sl.book)
                    .ToList();

                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-10}", "SupplyLine ID", "Supply ID", "Book Title", "Quantity");
                Console.WriteLine(new string('-', 65));

                foreach (var supplyLine in allSupplyLines)
                {
                    Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-10}", supplyLine.supplyLineId, supplyLine.supply.supplyId, supplyLine.book.title, supplyLine.quantity);
                }
            }
        }

        public SupplyLine GetSupplyLineById(Guid supplyLineId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                return db.GetTable<SupplyLine>()
                    .FirstOrDefault(sl => sl.supplyLineId == supplyLineId);
            }
        }

        public void AddSupplyLine(SupplyLine supplyLine)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Insert(supplyLine);
            }
        }

        public void UpdateSupplyLine(SupplyLine supplyLine)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.Update(supplyLine);
            }
        }

        public void DeleteSupplyLine(Guid supplyLineId)
        {
            using (var db = PostgreSQLTools.CreateDataConnection(_connectionString))
            {
                db.GetTable<SupplyLine>()
                    .Delete(sl => sl.supplyLineId == supplyLineId);
            }
        }
    }

}

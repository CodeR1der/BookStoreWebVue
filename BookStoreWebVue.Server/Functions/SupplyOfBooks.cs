using BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using Microsoft.Extensions.Configuration;

namespace BookStoreWebVue.Server.Functions
{
    public class SupplyOfBooks
    {

        public static void UpdateQuantityOnPost(SupplyLine line)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            using (var db = PostgreSQLTools.CreateDataConnection(configuration.GetConnectionString("MyConnectionString")))
            {
                var allSupplies = db.GetTable<SupplyLine>()
                    .LoadWith(req => req.supply)
                    .LoadWith(req => req.book)
                    .ToList();
                var allBookAvailabilaties = db.GetTable<BookAvailability>()
                    .LoadWith(req => req.warehouse)
                    .LoadWith(req => req.book)
                    .ToList();
                foreach (var availability in allBookAvailabilaties) 
                {
                    if (line.bookId == availability.bookId && line.supply.warehouseId == availability.warehouseId)
                    {
                        availability.quantity += line.quantity;
                    }
                }
            }
        }
        public static void UpdateQuantityOnUpdate(int oldQuantity, SupplyLine supplierLine)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            using (var db = PostgreSQLTools.CreateDataConnection(configuration.GetConnectionString("MyConnectionString")))
            {
                var allSupplies = db.GetTable<SupplyLine>()
                    .LoadWith(req => req.supply)
                    .LoadWith(req => req.book)
                    .ToList();
                var allBookAvailabilaties = db.GetTable<BookAvailability>()
                    .LoadWith(req => req.warehouse)
                    .LoadWith(req => req.book)
                    .ToList();
                var diff = Math.Abs(oldQuantity - supplierLine.quantity);
                foreach (var availability in allBookAvailabilaties)
                {
                    if (supplierLine.bookId == availability.bookId && supplierLine.supply.warehouseId == availability.warehouseId)
                    {
                        if(supplierLine.quantity > oldQuantity)
                        {
                            availability.quantity +=diff;
                        }
                        else
                        {
                            availability.quantity -= diff;

                        }
                    }
                }
            }
        }
    }
}

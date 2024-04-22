using BookStore;
using LinqToDB;
using LinqToDB.DataProvider.PostgreSQL;
using Microsoft.Extensions.Configuration;
using TestOperations;

namespace BookStoreWebVue.Server.Functions
{
    public class UpdateQuantityBooks
    {

        public static void UpdateQuantityOnPost(SupplyLine line)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            using (var db = PostgreSQLTools.CreateDataConnection(configuration.GetConnectionString("MyConnectionString")))
            {
                var avail = new BookAvailabilityDataAccess(configuration.GetConnectionString("MyConnectionString"));

                // Получаем все доступности книг по складу

                var supply = db.GetTable<Supply>()
                    .LoadWith(req => req.warehouse)
                    .FirstOrDefault(s => s.supplyId == line.supplyId);

                if (supply == null)
                {
                    return;
                }
                var book = line.book;
                var allBookAvailabilities = db.GetTable<BookAvailability>()
                    .LoadWith(req => req.warehouse)
                    .LoadWith(req => req.book)
                    .Where(availability => availability.warehouseId == supply.warehouseId)
                    .ToList();

                // Ищем существующую доступность книги по id книги
                var existingAvailability = allBookAvailabilities.FirstOrDefault(availability => availability.bookId == book.bookId);



                if (existingAvailability != null)
                {
                    // Обновляем количество книг
                    existingAvailability.quantity += line.quantity;
                    db.Update(existingAvailability);
                }
                else
                {
                    // Создаем новую доступность книги
                    var newAvailability = new BookAvailability
                    {
                        bookId = line.bookId,
                        warehouseId = supply.warehouseId,
                        quantity = line.quantity
                    };
                    avail.AddBookAvailability(newAvailability);
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
                var allBookAvailabilaties = db.GetTable<BookAvailability>()
                    .LoadWith(req => req.warehouse)
                    .LoadWith(req => req.book)
                    .ToList();
                var diff = Math.Abs(oldQuantity - supplierLine.quantity);
                var avail = new BookAvailabilityDataAccess(configuration.GetConnectionString("MyConnectionString"));
                foreach (var availability in allBookAvailabilaties)
                {
                    if (supplierLine.bookId == availability.bookId && supplierLine.supply.warehouseId == availability.warehouseId)
                    {
                        if (supplierLine.quantity > oldQuantity)
                        {
                            availability.quantity += diff;
                            avail.UpdateBookAvailability(availability);
                            break;
                        }
                        else
                        {
                            availability.quantity -= diff;
                            avail.UpdateBookAvailability(availability);
                            break;
                        }
                    }
                }
            }
        }
        public static void UpdateQuantityOrder(OrderLine line)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            using (var db = PostgreSQLTools.CreateDataConnection(configuration.GetConnectionString("MyConnectionString")))
            {
                var avail = new BookAvailabilityDataAccess(configuration.GetConnectionString("MyConnectionString"));
                var bookAvailabilaty = db.GetTable<BookAvailability>()
                   .LoadWith(req => req.warehouse)
                   .LoadWith(req => req.book)
                   .FirstOrDefault(availability => availability.bookId == line.bookId);
                bookAvailabilaty.quantity -= line.quantity;
                avail.UpdateBookAvailability(bookAvailabilaty);
            }
        }
    }
}

using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "BookAvailability")]
    public class BookAvailability
    {
        [PrimaryKey, Identity] public int availabilityId { get; set; }
        [Column] public int bookId { get; set; }
        [Association(ThisKey = nameof(bookId), OtherKey = nameof(Book.bookId))]
        public Book book { get; set;}
        [Column] public int warehouseId { get; set; }
        [Association(ThisKey = nameof(warehouseId), OtherKey = nameof(Warehouse.warehouseId))]
        public Warehouse warehouse { get; set; }
        [Column] public int quantity { get; set; }
    }
}

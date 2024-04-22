using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "OrderLine")]

    public class OrderLine
    {
        [PrimaryKey, Identity] public int lineID { get; set; }
        [Column] public int orderId { get; set; }
        [Association(ThisKey = nameof(orderId), OtherKey = nameof(CustomerOrder.orderId))]
        public CustomerOrder customerOrder { get; set; }
        [Column] public int bookId { get; set; }
        [Association(ThisKey = nameof(bookId), OtherKey = nameof(Book.bookId))]
        public Book book { get; set; }
        [Column] public double price { get; set; }
        [Column] public int quantity { get; set; }
    }
}

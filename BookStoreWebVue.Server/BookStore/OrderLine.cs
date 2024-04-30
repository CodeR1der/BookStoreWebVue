using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "OrderLine")]
    public class OrderLine
    {
        [PrimaryKey, Identity] public Guid lineID { get; set; }
        [Column] public Guid orderId { get; set; }
        [Association(ThisKey = nameof(orderId), OtherKey = nameof(CustomerOrder.orderId))]
        public CustomerOrder? customerOrder { get; set; }
        [Column] public Guid bookId { get; set; }
        [Association(ThisKey = nameof(bookId), OtherKey = nameof(Book.bookId))]
        public Book? book { get; set; }
        [Column] public int quantity { get; set; }
    }
}

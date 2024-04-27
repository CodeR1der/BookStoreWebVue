using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "SupplyLine")]

    public class SupplyLine
    {
        [PrimaryKey, Identity] public Guid supplyLineId { get; set; }
        [Column] public Guid supplyId { get; set; }
        [Association(ThisKey = nameof(supplyId), OtherKey = nameof(Supply.supplyId))]
        public Supply supply { get; set; }
        [Column] public Guid bookId { get; set; }
        [Association(ThisKey = nameof(bookId), OtherKey = nameof(Book.bookId))]
        public Book book { get; set; }
        [Column] public int quantity { get; set; }
    }
}

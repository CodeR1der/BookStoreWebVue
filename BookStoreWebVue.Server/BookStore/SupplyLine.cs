using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "SupplyLine")]

    public class SupplyLine
    {
        [PrimaryKey, Identity] public int supplyLineId { get; set; }
        [Column] public int supplyId { get; set; }
        [Association(ThisKey = nameof(supplyId), OtherKey = nameof(Supply.supplyId))]
        public Supply supply { get; set; }
        [Column] public int bookId { get; set; }
        [Association(ThisKey = nameof(bookId), OtherKey = nameof(Book.bookId))]
        public Book book { get; set; }
        [Column] public int quantity { get; set; }
    }
}

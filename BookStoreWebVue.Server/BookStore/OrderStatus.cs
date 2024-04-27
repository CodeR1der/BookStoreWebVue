using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "OrderStatus")]

    public class OrderStatus
    {
        [PrimaryKey, Identity] public Guid statusId { get; set; }
        [Column] public string statusValue { get; set; }
    }
}

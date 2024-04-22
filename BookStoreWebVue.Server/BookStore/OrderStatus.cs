using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "OrderStatus")]

    public class OrderStatus
    {
        [PrimaryKey, Identity] public int statusId { get; set; }
        [Column] public string statusValue { get; set; }
    }
}

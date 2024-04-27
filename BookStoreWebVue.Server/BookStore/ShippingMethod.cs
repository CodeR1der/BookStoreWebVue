using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "ShippingMethod")]

    public class ShippingMethod
    {
        [PrimaryKey, Identity] public Guid methodId { get; set; }
        [Column] public string methodName { get; set; }
        [Column] public double cost { get; set; }
    }
}

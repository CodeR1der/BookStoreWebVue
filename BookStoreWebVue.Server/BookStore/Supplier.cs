using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Supplier")]

    public class Supplier
    {
        [PrimaryKey, Identity] public Guid supplierId { get; set; }
        [Column] public string supplierName { get; set; }
    }
}

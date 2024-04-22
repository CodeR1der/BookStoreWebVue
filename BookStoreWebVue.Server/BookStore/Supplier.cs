using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "Supplier")]

    public class Supplier
    {
        [PrimaryKey, Identity] public int supplierId { get; set; }
        [Column] public string supplierName { get; set; }
    }
}

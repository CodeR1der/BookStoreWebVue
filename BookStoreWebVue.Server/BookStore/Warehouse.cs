using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "Warehouse")]

    public class Warehouse
    {
        [PrimaryKey,Identity] public int warehouseId { get; set; }
        [Column] public string warehouseName { get; set; }
        [Column] public int addressId { get; set; }
        [Association(ThisKey = nameof(addressId), OtherKey = nameof(Address.addressId))]
        public Address address { get; set; }

    }
}

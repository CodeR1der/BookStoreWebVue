using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Warehouse")]

    public class Warehouse
    {
        [PrimaryKey,Identity] public Guid warehouseId { get; set; }
        [Column] public string warehouseName { get; set; }
        [Column] public Guid addressId { get; set; }
        [Association(ThisKey = nameof(addressId), OtherKey = nameof(Address.addressId))]
        public Address address { get; set; }

    }
}

using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "Address")]
    public class Address
    {
        [PrimaryKey, Identity] public int addressId { get; set; }
        [Column] public string streetNumber { get; set; }
        [Column] public string streetName { get; set; }
        [Column] public string city { get; set; }
        [Column] public int countryId { get; set; }
        [Association(ThisKey = nameof(countryId), OtherKey = nameof(Country.countryId))]
        public Country country { get; set; }

    }
}

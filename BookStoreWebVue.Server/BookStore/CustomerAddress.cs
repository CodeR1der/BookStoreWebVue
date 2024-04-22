using LinqToDB.Mapping;

namespace BookStore
{
    [Table(Name = "CustomerAddress")]
    public class CustomerAddress
    {
        [Column] public int customerId { get; set; }
        [Association(ThisKey = nameof(customerId), OtherKey = nameof(Customer.customerId))]
        public Customer customer { get; set; }
        [Column] public int addressId { get; set; }
        [Association(ThisKey = nameof(addressId), OtherKey = nameof(Address.addressId))]
        public Address address { get; set; }

    }
}

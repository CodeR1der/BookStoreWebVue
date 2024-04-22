using LinqToDB.Mapping;

namespace BookStore
{
    [Table(Name = "CustomerOrder")]

    public class CustomerOrder
    {
        [PrimaryKey, Identity] public int orderId { get; set; }
        [Column] public DateTime orderDate { get; set; }
        [Column] public int customerId { get; set; }
        [Association(ThisKey = nameof(customerId), OtherKey = nameof(Customer.customerId))]
        public Customer customer { get; set; }
        [Column] public int shippingMethodId { get; set; }
        [Association(ThisKey = nameof(shippingMethodId), OtherKey = nameof(ShippingMethod.methodId))]
        public ShippingMethod shippingMethod { get; set; }
        [Column] public Address destAddressId { get; set; }
        [Association(ThisKey = nameof(destAddressId), OtherKey = nameof(Address.addressId))]
        public Address destAddress { get; set; }
    }
}

using LinqToDB.Mapping;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "CustomerOrder")]

    public class CustomerOrder
    {
        [PrimaryKey, Identity] public Guid orderId { get; set; }
        [Column] public DateTime orderDate { get; set; }
        [Column] public Guid customerId { get; set; }
        [Association(ThisKey = nameof(customerId), OtherKey = nameof(User.userId))]
        public User customer { get; set; }
        [Column] public Guid shippingMethodId { get; set; }
        [Association(ThisKey = nameof(shippingMethodId), OtherKey = nameof(ShippingMethod.methodId))]
        public ShippingMethod shippingMethod { get; set; }
        [Column] public Guid destAddressId { get; set; }
        [Association(ThisKey = nameof(destAddressId), OtherKey = nameof(Address.addressId))]
        public Address destAddress { get; set; }
    }
}

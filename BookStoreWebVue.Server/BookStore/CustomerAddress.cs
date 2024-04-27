using LinqToDB.Mapping;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "CustomerAddress")]
    public class CustomerAddress
    {
        [Column] public Guid customerId { get; set; }
        [Association(ThisKey = nameof(customerId), OtherKey = nameof(User.userId))]
        public User customer { get; set; }
        [Column] public Guid addressId { get; set; }
        [Association(ThisKey = nameof(addressId), OtherKey = nameof(Address.addressId))]
        public Address address { get; set; }

    }
}

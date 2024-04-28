using BookStoreWebVue.Server.BookStore;
using LinqToDB.Mapping;
using System.Net;

namespace BookStoreWebVue.Server.BookStore
{
    public class User
    {
        [PrimaryKey]public Guid userId { get; set; }
        [Column] public string email { get; set; }
        [Column] public string passwordHash { get; set; }
        [Column] public bool isAdmin { get; set; }
        [Column] public string nickname { get; set; }
        [Column] public string? firstName { get; set; }
        [Column] public string? lastName { get; set; }
        [Column] public string? customerAddress { get; set; }
        [Association(ThisKey = nameof(customerAddress), OtherKey = nameof(Address.addressId))]
        public Address address { get; set; }
    }
}

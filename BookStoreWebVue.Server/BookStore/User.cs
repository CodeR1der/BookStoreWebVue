using BookStoreWebVue.Server.BookStore;
using LinqToDB.Mapping;

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
    }
}

using LinqToDB.Mapping;


namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Author")]
    public class Author
    {
        [PrimaryKey, Identity] public Guid authorId { get; set; }
        [Column] public string authorName { get; set; }
    }
}

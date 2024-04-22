using LinqToDB.Mapping;


namespace BookStore
{
    [Table(Name = "Author")]
    public class Author
    {
        [PrimaryKey, Identity] public int authorId { get; set; }
        [Column] public string authorName { get; set; }
    }
}

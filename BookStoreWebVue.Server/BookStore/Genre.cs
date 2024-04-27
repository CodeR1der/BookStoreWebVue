using LinqToDB.Mapping;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Genre")]

    public class Genre
    {
        [PrimaryKey, Identity] public Guid genreId { get; set; }
        [Column] public string genreName { get; set; }
    }
}

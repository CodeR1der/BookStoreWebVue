using LinqToDB.Mapping;

namespace BookStore
{
    [Table(Name = "Genre")]

    public class Genre
    {
        [PrimaryKey, Identity] public int genreId { get; set; }
        [Column] public string genreName { get; set; }
    }
}

using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Publisher")]

    public class Publisher
    {
        [PrimaryKey, Identity] public Guid publisherId { get; set; }
        [Column] public string publisherName { get; set; }
    }
}

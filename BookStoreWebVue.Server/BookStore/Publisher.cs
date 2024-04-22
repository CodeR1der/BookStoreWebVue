using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "Publisher")]

    public class Publisher
    {
        [PrimaryKey, Identity] public int publisherId { get; set; }
        [Column] public string publisherName { get; set; }
    }
}

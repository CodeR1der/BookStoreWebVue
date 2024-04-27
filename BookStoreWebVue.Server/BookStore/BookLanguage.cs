using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "BookLanguage")]
    public class BookLanguage
    {
        [PrimaryKey, Identity] public Guid languageId { get; set; }
        [Column] public string? languageCode { get; set; }
        [Column] public string languageName { get; set; }
    }
}

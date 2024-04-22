using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "BookLanguage")]
    public class BookLanguage
    {
        [PrimaryKey, Identity] public int languageId { get; set; }
        [Column] public string languageCode { get; set; }
        [Column] public string languageName { get; set; }
    }
}

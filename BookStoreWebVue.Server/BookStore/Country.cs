using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Country")]
    public class Country
    {
        [PrimaryKey, Identity] public Guid countryId { get; set; }
        [Column] public string countryName { get; set; }
        public override string ToString()
        {
            return countryName;
        }
    }
}

using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "Country")]
    public class Country
    {
        [PrimaryKey, Identity] public int countryId { get; set; }
        [Column] public string countryName { get; set; }
        public override string ToString()
        {
            return countryName;
        }
    }
}

using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "Customer")]

    public class Customer
    {
        [PrimaryKey, Identity] public int customerId { get; set; }
        [Column] public string firstName { get; set; }
        [Column] public string lastName { get; set; }
        [Column] public string email { get; set; }
        
    }
}

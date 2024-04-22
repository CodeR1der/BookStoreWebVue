using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStore
{
    [Table(Name = "OrderHistory")]

    public class OrderHistory
    {
        [PrimaryKey, Identity] public int historyId { get; set; }
        [Column] public  int orderId { get; set; }
        [Association(ThisKey = nameof(orderId), OtherKey = nameof(CustomerOrder.orderId))]
        public CustomerOrder order { get; set; }
        [Column] public int statusId { get; set; }
        [Association(ThisKey = nameof(statusId), OtherKey = nameof(OrderStatus.statusId))]
        public OrderStatus status { get; set; }
        [Column] public DateTime statusDate { get; set; }
    }
}

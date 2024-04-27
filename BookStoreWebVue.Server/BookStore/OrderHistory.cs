using LinqToDB.Mapping;
using System.Xml.Linq;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "OrderHistory")]

    public class OrderHistory
    {
        [PrimaryKey, Identity] public Guid historyId { get; set; }
        [Column] public Guid orderId { get; set; }
        [Association(ThisKey = nameof(orderId), OtherKey = nameof(CustomerOrder.orderId))]
        public CustomerOrder order { get; set; }
        [Column] public Guid statusId { get; set; }
        [Association(ThisKey = nameof(statusId), OtherKey = nameof(OrderStatus.statusId))]
        public OrderStatus status { get; set; }
        [Column] public DateTime statusDate { get; set; }
    }
}

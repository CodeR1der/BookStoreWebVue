using LinqToDB.Mapping;

namespace BookStoreWebVue.Server.BookStore
{
    [Table(Name = "Supply")]

    public class Supply
    {
        [PrimaryKey, Identity] public Guid supplyId { get; set; }
        [Column] public Guid supplierId { get; set; }
        [Association(ThisKey = nameof(supplierId), OtherKey = nameof(Supplier.supplierId))]
        public Supplier supplier { get; set; }
        [Column] public Guid warehouseId { get; set; }
        [Association(ThisKey = nameof(warehouseId), OtherKey = nameof(Warehouse.warehouseId))]
        public Warehouse warehouse { get; set; }

    }
}

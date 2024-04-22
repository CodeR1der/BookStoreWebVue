using LinqToDB.Mapping;

namespace BookStore
{
    [Table(Name = "Supply")]

    public class Supply
    {
        [PrimaryKey, Identity] public int supplyId { get; set; }
        [Column] public int supplierId { get; set; }
        [Association(ThisKey = nameof(supplierId), OtherKey = nameof(Supplier.supplierId))]
        public Supplier supplier { get; set; }
        [Column] public int warehouseId { get; set; }
        [Association(ThisKey = nameof(warehouseId), OtherKey = nameof(Warehouse.warehouseId))]
        public Warehouse warehouse { get; set; }

    }
}

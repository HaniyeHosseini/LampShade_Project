using System;

namespace InventoryManagment.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public long Id { get; private set; }

        public bool Operation { get; private set; }
        public long Count { get; private set; }

        public long CurrentCount { get; private set; }

        public long OperatorId { get; private set; }

        public long InventoryId { get; private set; }

        public DateTime OperationDate { get; private set; }

        public string Description { get; private set; }

        public long OrderId { get; private set; }
        public Inventory Inventory { get; private set; }


        public InventoryOperation()
        {
                
        }

        public InventoryOperation(bool operation, long count, long currentCount, long operatorId, long inventoryId, string description, long orderId)
        {
            Operation = operation;
            Count = count;
            CurrentCount = currentCount;
            OperatorId = operatorId;
            InventoryId = inventoryId;
            Description = description;
            OrderId = orderId;
            OperationDate = DateTime.Now;
        }
    }
}

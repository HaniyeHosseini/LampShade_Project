using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Application.Contracts.Inventory
{
    public class IncreaseInventory
    {
        public long Count { get; set; }
        public long InventoryId { get; set; }
        public string Description { get; set; }

    }


}

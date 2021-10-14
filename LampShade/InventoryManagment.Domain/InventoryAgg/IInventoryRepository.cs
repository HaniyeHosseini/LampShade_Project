using _0_Framework.Application;
using _0_Framework.Domain;
using InventoryManagment.Application.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Domain.InventoryAgg
{
   public interface IInventoryRepository : IRepository<long , Inventory>
    {
        Inventory GetByProductId(long productId);
        EditInventory GetDeatails(long id);

        List<InventoryViewModel> Search(InventorySearchModel search);


        List<InventoryOperationViewModel> GetOperationLog(long inventoryid);


    }
}

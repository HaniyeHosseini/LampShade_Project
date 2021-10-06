using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Application.Contracts.Inventory
{
   public interface IInventoryApplication
    {

        OperationResult Create(CreateInventory command);

        OperationResult Edit(EditInventory command);

        OperationResult Increaes(IncreaseInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        OperationResult Reduce(ReduceInventory command);



        List<InventoryViewModel> Search(InventorySearchModel search);

        EditInventory GetDeatails(long id);


    }
}

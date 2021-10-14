using _0_Framework.Application;
using InventoryManagment.Application.Contracts.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Application
{
    public class InventoryApplication : IInventoryApplication
    {

        private readonly IInventoryRepository inventoryrepository;

        public InventoryApplication(IInventoryRepository inventoryrepository)
        {
            this.inventoryrepository = inventoryrepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operationresult = new OperationResult();
            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            if (inventoryrepository.Exist(x => x.ProdctId == command.ProductId))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);
            inventoryrepository.Create(inventory);
            return operationresult.Succedded();

        }

        public OperationResult Edit(EditInventory command)
        {
            var operationresult = new OperationResult();
            var inventory = inventoryrepository.GetByProductId(command.ProductId);
            if (inventory == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            if (inventoryrepository.Exist(x => x.ProdctId == command.ProductId && x.Id != command.Id))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            inventoryrepository.Save();
            return operationresult.Succedded();


        }

        public EditInventory GetDeatails(long id)
        {
            return inventoryrepository.GetDeatails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryid)
        {
            return inventoryrepository.GetOperationLog(inventoryid);
        }

        public OperationResult Increaes(IncreaseInventory command)
        {
            const long operatorid = 1;
            var operationresult = new OperationResult();
            var inventory = inventoryrepository.GetByProductId(command.ProductId);
            if (inventory == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            inventory.Increase(command.Count, operatorid, command.Description);
            inventoryrepository.Save();
            return operationresult.Succedded();

        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operationresult = new OperationResult();

            const long operatorid = 1;
            foreach (var item in command)
            {
                var inventory = inventoryrepository.GetByProductId(item.ProductId);
                inventory.Reduce(item.Count, operatorid, item.Description, item.OrderId);
            }
            inventoryrepository.Save();

            return operationresult.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
          
            var operationresult = new OperationResult();
            var inventory = inventoryrepository.GetByProductId(command.ProductId);
            if (inventory == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            inventory.Reduce(command.Count,0,command.Description,command.OrderId);
            inventoryrepository.Save();
            return operationresult.Succedded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel search)
        {
            return inventoryrepository.Search(search);
        }
    }
}

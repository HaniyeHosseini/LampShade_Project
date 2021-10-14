using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagment.Application.Contracts.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Infrastructure.EFCore.Repositories
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext context;
        private readonly ShopContext shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            this.context = context;
            this.shopContext = shopContext;
        }

        public EditInventory GetDeatails(long id)
        {
            return context.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                ProductId = x.ProdctId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel search)
        {

            var products = shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                 ProductId=x.ProdctId,
                 CreationDate=x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount(),
              
            });

            if (search.ProductId > 0)
                query = query.Where(x => x.ProductId == search.ProductId);

            if (search.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId).Name);

            return inventory;



        }
        public  Inventory GetByProductId(long productId)
        {
            var inventory = context.Inventory.FirstOrDefault(x => x.ProdctId == productId);
            return inventory;
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryid)
        {
            var inventory = context.Inventory.FirstOrDefault(x => x.Id == inventoryid);

            return inventory.Operations.Select(x => new InventoryOperationViewModel()
            {

                Id = x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Operation = x.Operation,
                OperatorId = x.OperatorId,
                OrderId = x.OrderId,
                OperationDate=x.OperationDate.ToFarsi(),
                Operator="مدیر سیستم",
            }).OrderByDescending(x=>x.Id).ToList();



        }
    }
}

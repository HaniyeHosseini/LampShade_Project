using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagment.Application.Contracts.ColleagueDiscount;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using InventoryManagment.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Adminstration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryApplication inventoryApplication;
        private readonly IProductApplication productApplication;
        public InventorySearchModel searchModel { get; set; }
        public List<InventoryViewModel> Inventoriy { get; set; }
        public SelectList Products { get; set; }

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            this.inventoryApplication = inventoryApplication;
            this.productApplication = productApplication;
        }


        public void OnGet(InventorySearchModel search)
        {
            Products = new SelectList(productApplication.GetProducts(), "Id", "Name");

            Inventoriy = inventoryApplication.Search(search);

        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory() {
                Products = productApplication.GetProducts(),
            
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
          var result=  inventoryApplication.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var inventory = inventoryApplication.GetDeatails(id);
            inventory.Products = productApplication.GetProducts();
            return Partial("./Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = inventoryApplication.Edit(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                ProductId = id,
                
            };
            return Partial("./Increase", command);
        }

        public IActionResult OnPostIncrease(IncreaseInventory command)
        {
            var result = inventoryApplication.Increaes(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetReduce(long id)
        {
            var command = new  ReduceInventory()
            {
                ProductId=id,
            };
            return Partial("./Reduce", command);
        }

        public IActionResult OnPostReduce(ReduceInventory command)
        {
            var result = inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetLog(long id)
        {
            var log = inventoryApplication.GetOperationLog(id);
            return Partial("./OperationLog", log);


        }


    }
}

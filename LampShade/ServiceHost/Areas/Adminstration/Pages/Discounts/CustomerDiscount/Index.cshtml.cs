using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Adminstration.Pages.Discounts.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly ICustomerDiscountApplication customerDiscountApplication;
        private readonly IProductApplication productApplication;
        
        public CustomerDiscountSearchModel searchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }

        public SelectList Products { get; set; }

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            this.productApplication = productApplication;
            this.customerDiscountApplication = customerDiscountApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)

        {
            Products = new SelectList(productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = customerDiscountApplication.Search(searchModel);


        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = productApplication.GetProducts(),
            };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = customerDiscountApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var customerdiscount = customerDiscountApplication.GetDeatails(id);
            customerdiscount.Products = productApplication.GetProducts();
            return Partial("./Edit", customerdiscount);

        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = customerDiscountApplication.Edit(command);
            return new JsonResult(result);

        }

   
    }
}

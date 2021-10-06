using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagment.Application.Contracts.ColleagueDiscount;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Adminstration.Pages.Discounts.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly IColleagueDiscountApplication colleagueDiscountApplication;
        private readonly IProductApplication productApplication;

        public ColleagueDiscountSearchModel searchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }

        public SelectList Products { get; set; }

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            this.productApplication = productApplication;
            this.colleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)

        {
            Products = new SelectList(productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = colleagueDiscountApplication.Search(searchModel);


        }
        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = productApplication.GetProducts(),
            };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var colleaguediscount = colleagueDiscountApplication.GetDeatails(id);
            colleaguediscount.Products = productApplication.GetProducts();
            return Partial("./Edit", colleaguediscount);

        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetRemove(long id)
        {
           var DIS= colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index",DIS);

        }
        public IActionResult OnGetRestore(long id)
        {
          var DIS=  colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index",DIS);

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Application.Contracts.ProductCategory;

namespace ServiceHost.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        private readonly IProductApplication productApplication;
        private readonly IProductCategoryApplication productCategoryApplication;
        public SelectList ProductCategories;
        public ProductpictureSearchModel searchModel { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            this.productApplication = productApplication;
            this.productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductpictureSearchModel searchModel)
        {
            Products = productApplication.Search(searchModel);
            ProductCategories = new SelectList(productCategoryApplication.GetProductCategories(), "Id", "Name");

        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories=productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = productApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var product = productApplication.GetDeatails(id);
            product.Categories = productCategoryApplication.GetProductCategories();
            return Partial("./Edit", product);

        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = productApplication.Edit(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetNotInStock(long Id)
        {
           var result= productApplication.NotInStock(Id);
            if (result.IsSuccedded)
               return RedirectToPage("./Index");
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }
        public IActionResult OnGetIsInStock(long Id)
        {
           var result= productApplication.InStock(Id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }
    }
}

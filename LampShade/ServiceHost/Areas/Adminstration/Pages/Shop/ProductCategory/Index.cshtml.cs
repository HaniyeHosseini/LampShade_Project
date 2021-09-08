using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagment.Application.Contracts.ProductCategory;

namespace ServiceHost.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication productCategoryApplication;

        public ProductCategorySearchModel searchModel { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            this.productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = productCategoryApplication.Search(searchModel);


        }
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
          var result = productCategoryApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var productcategory = productCategoryApplication.GetDeatails(id);
          
            return Partial("./Edit", productcategory);

        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = productCategoryApplication.Edit(command);
            return new JsonResult(result);

        }

    }
}

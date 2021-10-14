using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery productCategoryQuery;
        public ProductCategoryQueryModel ProductCategory { get; set; }
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            this.productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            ProductCategory = productCategoryQuery.GetProductCategoryWithProductBy(id);
           

        }
    }
}

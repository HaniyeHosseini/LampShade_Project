using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery productcategoryquery;

        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productcategoryquery)
        {
            this.productcategoryquery = productcategoryquery;
        }

        public IViewComponentResult Invoke()
        {
            var categories = productcategoryquery.GetProductCategoryWithProduct();
            return View(categories);
        }

    }
}

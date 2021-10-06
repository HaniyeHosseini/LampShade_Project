using _01_LampShadeQuery.Contracts.ProductCategory;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext shopContext;

        public ProductCategoryQuery(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Name=x.Name,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Slug=x.Slug,
                Id=x.Id
            }).ToList();
        }
    }
}

using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using DiscountManagment.Infrastructure.EFCore;
using InventoryManagment.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Domain.ProductAgg;
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
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            this.shopContext = shopContext;
            this.inventoryContext = inventoryContext;
            this.discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Id = x.Id
            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoryWithProduct()
        {
            var inventory = inventoryContext.Inventory.Select(x => new { x.ProdctId, x.UnitPrice }).ToList();
            var discounts = discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var categories = shopContext.ProductCategories.Include(x => x.Products).ThenInclude(x => x.Category)
                  .Select(x => new ProductCategoryQueryModel()
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Products = MapProducts(x.Products)


                  }).AsNoTracking().ToList();

            foreach (var category in categories)
            {

                foreach (var product in category.Products)
                {

                    var _inventory = inventory.FirstOrDefault(x => x.ProdctId == product.Id);
                    if (_inventory != null)
                    {
                        var price = inventory.FirstOrDefault(x => x.ProdctId == product.Id).UnitPrice;
                        product.Price = price.ToMoney();
                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            var discountRate = discount.DiscountRate;
                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);
                            var pricewithdiscount = Math.Round(price - discountAmount);
                            product.PriceWithDiscount = pricewithdiscount.ToMoney();

                        }
                    }




                }
            }


            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel()
            {
                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Category = product.Category.Name,
                Slug = product.Slug,



            }).ToList();




        }

        public ProductCategoryQueryModel GetProductCategoryWithProductBy(string slug)
        {
            var inventory = inventoryContext.Inventory.Select(x => new { x.ProdctId, x.UnitPrice }).ToList();
            var discounts = discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate,x.EndDate }).ToList();
            var category = shopContext.ProductCategories.Include(x => x.Products).ThenInclude(x => x.Category)
                  .Select(x => new ProductCategoryQueryModel()
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Description = x.Description,
                      Keywords = x.Keywords,
                      MetaDescription = x.MetaDescription,
                      Slug= x.Slug,
                      Products = MapProducts(x.Products),
                  }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);



            foreach (var product in category.Products)
            {

                var _inventory = inventory.FirstOrDefault(x => x.ProdctId == product.Id);
                if (_inventory != null)
                {
                    var price = inventory.FirstOrDefault(x => x.ProdctId == product.Id).UnitPrice;
                    product.Price = price.ToMoney();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        var discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        product.DiscountExpiredDate = discounts.FirstOrDefault(x => x.ProductId == product.Id).EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        var pricewithdiscount = Math.Round(price - discountAmount);
                        product.PriceWithDiscount = pricewithdiscount.ToMoney();

                    }
                }
            }

            return category;
        }
    }
}

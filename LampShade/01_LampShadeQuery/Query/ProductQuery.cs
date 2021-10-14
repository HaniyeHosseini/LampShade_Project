using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Product;
using DiscountManagment.Infrastructure.EFCore;
using InventoryManagment.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext shopContext;
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;
        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            this.shopContext = shopContext;
            this.inventoryContext = inventoryContext;
            this.discountContext = discountContext;
        }


        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = inventoryContext.Inventory.Select(x => new { x.ProdctId, x.UnitPrice }).ToList();
            var discounts = discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate , x.EndDate }).ToList();
            var products = shopContext.Products.Select(product => new ProductQueryModel() {

                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Category = product.Category.Name,
              
                Slug = product.Slug,
                
            }).AsNoTracking().OrderByDescending(x=>x.Id).Take(6).ToList();

            foreach (var product in products)
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
                        product.DiscountExpiredDate = discount.EndDate.ToDiscountFormat();
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        var pricewithdiscount = Math.Round(price - discountAmount);
                        product.PriceWithDiscount = pricewithdiscount.ToMoney();

                    }
                }




            }


            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = inventoryContext.Inventory.Select(x => new { x.ProdctId, x.UnitPrice }).ToList();
            var discounts = discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
            var query = shopContext.Products.Include(x => x.Category)
                  .Select(x => new ProductQueryModel()
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Picture = x.Picture,
                      PictureAlt = x.PictureAlt,
                      PictureTitle = x.PictureTitle,
                      Category = x.Category.Name,
                      CategorySlug=x.Category.Slug, 
                      Slug = x.Slug,
                      ShortDescrption=x.ShortDescription
                  }).AsNoTracking();


            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescrption.Contains(value));
            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
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

            return products;
        }
    }
}

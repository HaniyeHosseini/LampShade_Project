using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditProduct GetDeatails(long Id)
        {
            return _context.Products.Select(x => new EditProduct()
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                Code = x.Code,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
            }).FirstOrDefault();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel()
            {
                Id=x.Id,
                Name=x.Name
             
            }).ToList();
          
          
        }

        public List<ProductViewModel> Search(ProductpictureSearchModel search)
        {
            var query = _context.Products.Include(x=>x.Category).Select(x => new ProductViewModel()
            {
                Name =x.Name,
                Code=x.Code,
                CategoryId=x.CategoryId,
                Category=x.Category.Name,
                Id=x.Id,
                Picture=x.Picture,
                UnitPrice=x.UnitPrice,
                CreationDate=x.CreationDate.ToFarsi(),
                IsInStock=x.IsInStock,
                
            });
            if (!string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.Contains(search.Name));


            if (!string.IsNullOrWhiteSpace(search.Code))
                query = query.Where(x => x.Code.Contains(search.Code));

          if(search.CategoryId != 0)
                query = query.Where(x => x.CategoryId == search.CategoryId);


          return  query.OrderByDescending(x => x.Id).ToList();

         

            
        }
    }
}

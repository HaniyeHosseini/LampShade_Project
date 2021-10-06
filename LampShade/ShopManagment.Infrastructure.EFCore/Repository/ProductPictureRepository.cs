using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDeatails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture()

            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId


            }).FirstOrDefault(x => x.Id == id);

        }


        public List<ProductPictureViewModel> Search(ProductPictureSearchModel search)
        {
            var query = _context.ProductPictures.Include(x=>x.Product).Select(x => new ProductPictureViewModel()
            {

                Id=x.Id,
                CreationDate=x.CreationDate.ToFarsi(),
                Picture=x.Picture,
                Product=x.Product.Name,
                ProductId=x.ProductId,
                IsRemoved=x.IsRemoved,
            });


            if (search.ProductId != 0)
                query = query.Where(x => x.ProductId == search.ProductId);


            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}

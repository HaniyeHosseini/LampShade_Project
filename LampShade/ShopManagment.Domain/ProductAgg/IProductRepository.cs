using _0_Framework.Domain;
using ShopManagment.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Domain.ProductAgg
{
   public interface IProductRepository : IRepository<long , Product>
    {
        EditProduct GetDeatails(long Id);
        List<ProductViewModel> Search(ProductpictureSearchModel search);
        List<ProductViewModel> GetProducts();


    }
}

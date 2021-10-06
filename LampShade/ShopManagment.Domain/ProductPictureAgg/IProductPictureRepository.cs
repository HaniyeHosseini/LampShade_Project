using _0_Framework.Domain;
using ShopManagment.Application.Contracts.ProductPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository :IRepository<long,ProductPicture>
    {
        EditProductPicture GetDeatails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel search);
    }
}

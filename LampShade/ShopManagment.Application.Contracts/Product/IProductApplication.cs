using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);

        OperationResult InStock(long Id);
        OperationResult NotInStock(long Id);

        EditProduct GetDeatails(long Id);
        List<ProductViewModel> GetProducts();
        List<ProductViewModel> Search(ProductpictureSearchModel search);

    }
}

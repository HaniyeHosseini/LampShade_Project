using _0_Framework.Application;
using ShopManagment.Application.Contracts.ProductCategory;
using System.Collections.Generic;

namespace ShopManagment.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication 
    {
        OperationResult Create(CreateProductCategory entiti);
        OperationResult Edit(EditProductCategory ebtiti);
       

        EditProductCategory GetDeatails(long id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);


    }
}

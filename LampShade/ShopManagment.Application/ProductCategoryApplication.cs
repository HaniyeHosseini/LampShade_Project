using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagment.Application.Contracts.ProductCategory;

using ShopManagment.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory entiti)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exist(x => x.Name == entiti.Name))
            { return operation.Failed(""); }

            var Slug = entiti.Slug.slufigy();

            var path = $"{Slug}";
            var fileName = _fileUploader.Upload(entiti.Picture, path);

            var productCategory = new ProductCategory(entiti.Name, entiti.Description,fileName,
               entiti.PictureAlt, entiti.PictureTitle, entiti.Keywords, entiti.MetaDescription, Slug);
            _productCategoryRepository.Create(productCategory); 
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductCategory entiti)
        {
            var slug = entiti.Slug.slufigy();
            var operation = new OperationResult();
            var productcategory = _productCategoryRepository.GetBy(entiti.Id);
            if (productcategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_productCategoryRepository.Exist(x => x.Name == entiti.Name && x.Id != entiti.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var pathPicture = $"{slug}";
            var fileName = _fileUploader.Upload(entiti.Picture,pathPicture);

            productcategory.Edit(entiti.Name, entiti.Description,fileName, entiti.Keywords,
                entiti.MetaDescription, slug, entiti.PictureAlt, entiti.PictureTitle);

            _productCategoryRepository.Save();
           return operation.Succedded();

        }

        public EditProductCategory GetDeatails(long id)
        {
            return _productCategoryRepository.GetDeatails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
           var categories= _productCategoryRepository.GetProductCategories();
            return categories;
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}

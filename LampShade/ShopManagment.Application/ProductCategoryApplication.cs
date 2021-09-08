﻿using _0_Framework.Application;
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
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory entiti)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exist(x => x.Name == entiti.Name))
            { return operation.Failed("امکان ثبت مقدار تکراری وجود ندارد"); }

            var Slug = entiti.Slug.slufigy();
                
           var productCategory = new ProductCategory(entiti.Name, entiti.Description, entiti.Picture,
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
                return operation.Failed("این رکورد وجود ندارد");
            if (_productCategoryRepository.Exist(x => x.Name == entiti.Name && x.Id != entiti.Id))
                return operation.Failed("این رکورد تکراری است");

            productcategory.Edit(entiti.Name, entiti.Description, entiti.Picture, entiti.Keywords,
                entiti.MetaDescription, slug, entiti.PictureAlt, entiti.PictureTitle);

            _productCategoryRepository.Save();
           return operation.Succedded();

        }

        public EditProductCategory GetDeatails(long id)
        {
            return _productCategoryRepository.GetDeatails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}

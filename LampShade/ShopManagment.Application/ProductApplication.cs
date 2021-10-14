using _0_Framework.Application;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;
using ShopManagment.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application
{
    public class ProductApplication : IProductApplication
    { private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository productRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            this.productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operationresult = new OperationResult();
            if (productRepository.Exist(x => x.Name == command.Name))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.slufigy();
            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var product = new Product(command.Name, command.Code, command.ShortDescription, command.Description,picturePath,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug, command.CategoryId);

            productRepository.Create(product);
            return operationresult.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            var slug = command.Slug.slufigy();
            var operationresult = new OperationResult();
            var product = productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            if (productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);
            var path = $"{product.Category.Slug}//{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code, command.ShortDescription, command.Description, picturePath,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug, command.CategoryId);
            productRepository.Save();

            return operationresult.Succedded();

        }

        public EditProduct GetDeatails(long Id)
        {
            return productRepository.GetDeatails(Id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return productRepository.GetProducts(); 
        }

       

        public List<ProductViewModel> Search(ProductpictureSearchModel search)
        {
            return productRepository.Search(search);
        }
    }
}

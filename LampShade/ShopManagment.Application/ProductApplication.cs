using _0_Framework.Application;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operationresult = new OperationResult();
            if (productRepository.Exist(x => x.Name == command.Name))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.Slug, command.CategoryId);

            productRepository.Create(product);
            return operationresult.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            var slug = command.Slug.slufigy();
            var operationresult = new OperationResult();
            var product = productRepository.GetBy(command.Id);
            if (product == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            if (productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription, command.Description, command.Picture,
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

        public OperationResult InStock(long Id)
        {
            var operationresult = new OperationResult();
            var product = productRepository.GetBy(Id);
            if (product == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);
            product.InStock();
            productRepository.Save();
            return operationresult.Succedded();
        }

        public OperationResult NotInStock(long Id)
        {
            var operationresult = new OperationResult();
            var product = productRepository.GetBy(Id);
            if (product == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);
            product.NotInStock();
            productRepository.Save();
            return operationresult.Succedded();
        }

        public List<ProductViewModel> Search(ProductpictureSearchModel search)
        {
            return productRepository.Search(search);
        }
    }
}

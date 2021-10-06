using _0_Framework.Application;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application
{
   public class ProudctPictureApplication : IProductPictureApplication
    {

        private readonly IProductPictureRepository productPictureRepository;

        public ProudctPictureApplication(IProductPictureRepository productPictureRepository)
        {
            this.productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operationresult = new OperationResult();

            if (productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId==command.ProductId))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            var productpicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            productPictureRepository.Create(productpicture);
            return operationresult.Succedded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationresult = new OperationResult();

            var productpicture = productPictureRepository.GetBy(command.Id);

            if (productpicture == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            if (productPictureRepository.Exist(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            productpicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            productPictureRepository.Save();
            return operationresult.Succedded();
                

        }

        public EditProductPicture GetDeatails(long id)
        {
            return productPictureRepository.GetDeatails(id);
        }

        public OperationResult Remove(long id)
        {
            var operationresult = new OperationResult();

            var productpicture = productPictureRepository.GetBy(id);

            if (productpicture == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            productpicture.Remove();
            productPictureRepository.Save();
            return operationresult.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operationresult = new OperationResult();

            var productpicture = productPictureRepository.GetBy(id);

            if (productpicture == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            productpicture.Restore();
            productPictureRepository.Save();
            return operationresult.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel search)
        {
            return productPictureRepository.Search(search);
        }
    }
}

using _0_Framework.Application;
using ShopManagment.Application.Contracts.Slide;
using ShopManagment.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            this.slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operationresult = new OperationResult();
            Slide slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading, command.Title
                                    , command.Text, command.BtnText,command.Link);

            slideRepository.Create(slide);
            return operationresult.Succedded();
           
        }

        public OperationResult Edit(EditSlide command)
        {
            var operationresult = new OperationResult();

            var slide = slideRepository.GetBy(command.Id);

            if (slide == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading, command.Title, command.Text, command.BtnText,command.Link);
            slideRepository.Save();
            return operationresult.Succedded();


        }

        public EditSlide GetDeatails(long id)
        {
          return  slideRepository.GetDeatails(id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return slideRepository.GetSlides();
        }

        public OperationResult Remove(long id)
        {
            OperationResult operationresult = new OperationResult();
            var slide = slideRepository.GetBy(id);
            if (slide == null)
                operationresult.Failed(ApplicationMessages.RecordNotFound);
            slide.Remove();
            slideRepository.Save();
            return operationresult.Succedded();
        }

        public OperationResult Restore(long id)
        {
            OperationResult operationresult = new OperationResult();
            var slide = slideRepository.GetBy(id);
            if (slide == null)
                operationresult.Failed(ApplicationMessages.RecordNotFound);
            slide.Restore();
            slideRepository.Save();
            return operationresult.Succedded();
        }
    }
}

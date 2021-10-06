using _0_Framework.Domain;
using ShopManagment.Application.Contracts.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long,Slide>
    {
        EditSlide GetDeatails(long id);

        List<SlideViewModel> GetSlides();
    }
}

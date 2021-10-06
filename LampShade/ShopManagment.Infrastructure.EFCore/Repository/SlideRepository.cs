using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagment.Application.Contracts.Slide;
using ShopManagment.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {

        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDeatails(long id)
        {
            return _context.Slides.Select(x => new EditSlide()
            {
                BtnText=x.BtnText,
                Heading=x.Heading,
                Id=x.Id,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Text=x.Text,
                Title=x.Title,
                Link=x.Link

            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _context.Slides.Select(x => new SlideViewModel()
            {
                Heading=x.Heading,
                Id=x.Id,
                Picture=x.Picture,
                Title=x.Title,
                CreationDate=x.CreationDate.ToFarsi(),
                IsRemoved=x.IsRemoved,
            }).OrderByDescending(x=>x.Id).ToList();


        }
    }
}

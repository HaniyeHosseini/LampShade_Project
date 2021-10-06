using _01_LampShadeQuery.Contracts.Slide;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides.Where(x=>x.IsRemoved==false).Select(x => new SlideQueryModel()
            {
                Text = x.Text,
                BtnText = x.BtnText,
                Heading = x.Heading,
                IsRemoved = x.IsRemoved,
                Link = x.Link,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Title = x.Title,
            }).ToList();

        }
    }
}

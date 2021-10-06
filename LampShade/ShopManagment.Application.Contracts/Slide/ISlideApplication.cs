﻿using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);

        OperationResult Remove(long id);

        OperationResult Restore(long id);

        EditSlide GetDeatails(long id);

        List<SlideViewModel> GetSlides();

    }
}

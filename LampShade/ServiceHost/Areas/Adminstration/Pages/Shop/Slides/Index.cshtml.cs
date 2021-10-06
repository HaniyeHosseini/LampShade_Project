using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Application.Contracts.ProductCategory;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Application.Contracts.Slide;

namespace ServiceHost.Areas.Adminstration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        private readonly ISlideApplication slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            this.slideApplication = slideApplication;
        }

        public List<SlideViewModel> Slides { get; set; }
       



        public void OnGet(ProductPictureSearchModel searchModel)
        {


            Slides = slideApplication.GetSlides();
         

        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
         
          
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = slideApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var slide = slideApplication.GetDeatails(id);
           
           
            return Partial("./Edit", slide);

        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = slideApplication.Edit(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetRemove(long id)
        {
           var result= slideApplication.Remove(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");


        }
        public IActionResult OnGetRestore(long id)
        {
            var result = slideApplication.Restore(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


    }
}

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

namespace ServiceHost.Areas.Adminstration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        private readonly IProductPictureApplication productpictureApplication;
        private readonly IProductApplication productApplication;


        public IndexModel(IProductPictureApplication productpictureApplication, IProductApplication productApplication)
        {
            this.productpictureApplication = productpictureApplication;
            this.productApplication = productApplication;
        }

        public ProductPictureSearchModel searchModel { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public SelectList Products { get; set; }



        public void OnGet(ProductPictureSearchModel searchModel)
        {

            Products = new SelectList(productApplication.GetProducts(),"Id" , "Name");
            ProductPictures = productpictureApplication.Search(searchModel).ToList();
         

        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = productApplication.GetProducts()
            };
          
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = productpictureApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var productpicture = productpictureApplication.GetDeatails(id);
            productpicture.Products = productApplication.GetProducts();
           
            return Partial("./Edit", productpicture);

        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = productpictureApplication.Edit(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetRemove(long id)
        {
           var result= productpictureApplication.Remove(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");


        }
        public IActionResult OnGetRestore(long id)
        {
            var result = productpictureApplication.Restore(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


    }
}

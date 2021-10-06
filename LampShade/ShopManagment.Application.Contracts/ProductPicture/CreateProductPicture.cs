using _0_Framework.Application;
using ShopManagment.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
       [Range(1,100000)]
        public long ProductId { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        public List<ProductViewModel> Products { get;  set; }
    }


}

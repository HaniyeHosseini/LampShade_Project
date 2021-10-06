
using _0_Framework.Application;
using ShopManagment.Application.Contracts.ProductCategory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagment.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public double UnitPrice { get; set; }


        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public string ShortDescription { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public string Description { get; set; }

        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        [Range(1,100000,ErrorMessage =ValidationMessage.IsRequired)]
        public long CategoryId { get;  set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public string MetaDescription { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]

        public string Slug { get; set; }

        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}

using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace ShopManagment.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }
        public string Description { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtentionLimitationAttribut(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]

        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }



        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }


        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

    }
}

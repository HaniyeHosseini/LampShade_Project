using _0_Framework.Domain;
using ShopManagment.Domain.ProductCategoryAgg;
using ShopManagment.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Domain.ProductAgg
{
    public class Product : EntitiBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
   
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }

        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get;private set; }


        public Product()
        {
                
        }

        public Product(string name, string code, string shortDescription, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords
            , string metaDescription, string slug, long categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CategoryId = categoryId;
            ProductPictures = new List<ProductPicture>();
        }



        public void Edit(string name, string code, string shortDescription, string description,
           string picture, string pictureAlt, string pictureTitle, string keywords
           , string metaDescription, string slug, long categoryId)
        {
            Name = name;
            Code = code;
 
            ShortDescription = shortDescription;
         
            Description = description;
            if(!string.IsNullOrWhiteSpace(picture))

            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CategoryId = categoryId;
        }



     
    }
}

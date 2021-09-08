using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagment.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntitiBase
    {
        public string Name { get;private set; }
        public string Description { get; private set; }

        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        public ProductCategory()
        {

        }
        public ProductCategory(string name, string description, string picture
                                , string pictureAlt, string pictureTitle
                                ,string keywords,string metadescription,string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metadescription;
        }

        public void Edit (string name, string description, string picture,
                          string keywords, string metadescription, string slug
                          , string pictureAlt, string pictureTitle)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metadescription;
        }
    }
}

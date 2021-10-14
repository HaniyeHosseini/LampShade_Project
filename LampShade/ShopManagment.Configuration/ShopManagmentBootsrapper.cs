using Microsoft.Extensions.DependencyInjection;
using ShopManagment.Application;
using ShopManagment.Application.Contracts.ProductCategory;
using ShopManagment.Domain.ProductCategoryAgg;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagment.Infrastructure.EFCore.Repository;
using ShopManagment.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Domain.ProductPictureAgg;
using ShopManagment.Application.Contracts.Slide;
using ShopManagment.Domain.SlideAgg;
using _01_LampShadeQuery.Contracts.Slide;
using _01_LampShadeQuery.Query;
using _01_LampShadeQuery.Contracts.ProductCategory;
using _01_LampShadeQuery.Contracts.Product;

namespace ShopManagment.Configuration
{
    public class ShopManagmentBootsrapper
    {

        public static void  Configure(IServiceCollection services , string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductPictureApplication, ProudctPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideQuery, SlideQuery>();

            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();


            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}

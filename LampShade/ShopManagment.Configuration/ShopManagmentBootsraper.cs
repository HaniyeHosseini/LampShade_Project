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

namespace ShopManagment.Configuration
{
    public class ShopManagmentBootsraper
    {

        public static void  Configure(IServiceCollection services , string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}

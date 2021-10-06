using InventoryManagment.Application;
using InventoryManagment.Application.Contracts.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using InventoryManagment.Infrastructure.EFCore;
using InventoryManagment.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InventoryManagment.Infrastructure.Configuration
{
    public class InventoryManagmentBootsrapper
    {

        public static void Configure(IServiceCollection services , string connectionstring)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}

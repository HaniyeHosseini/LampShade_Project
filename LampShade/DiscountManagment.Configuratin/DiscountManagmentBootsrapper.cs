using DiscountManagment.Application;
using DiscountManagment.Application.Contracts.ColleagueDiscount;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using DiscountManagment.Domain.ColeagueDiscountAgg;
using DiscountManagment.Domain.CustomerDiscountAgg;
using DiscountManagment.Infrastructure.EFCore;
using DiscountManagment.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiscountManagment.Configuratin
{
    public class DiscountManagmentBootsrapper
    {

        public static void Configure(IServiceCollection services , string connectionstring)
        {
            services.AddDbContext<DiscountContext>(z=>z.UseSqlServer(connectionstring));
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddTransient<IColleagueRepository, ColleagueDiscountRepository>();
            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();

        }
    }
}

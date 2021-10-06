using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagment.Application.Contracts.ColleagueDiscount;
using DiscountManagment.Domain.ColeagueDiscountAgg;
using ShopManagment.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Infrastructure.EFCore.Repositories
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueRepository
    {
        private readonly DiscountContext discountContext;

        private readonly ShopContext shopContext;

        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            this.discountContext = discountContext;
            this.shopContext = shopContext;
        }

        public EditColleagueDiscount GetDeatails(long Id)
        {
            return discountContext.ColleagueDiscounts.Select(x => new EditColleagueDiscount() 
            {Id=x.Id,
            DiscountRate=x.DiscountRate,
            ProductId=x.ProductId,
          
            
            }).FirstOrDefault(x=>x.Id==Id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = discountContext.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
                
                IsRemoved = x.IsRemoved
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount =>
                discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return discounts;

        }
    }
}

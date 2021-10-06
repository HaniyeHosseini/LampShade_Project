using _0_Framework.Domain;
using DiscountManagment.Application.Contracts.ColleagueDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Domain.ColeagueDiscountAgg
{
    public interface IColleagueRepository : IRepository<long, ColleagueDiscount>
    {
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);

        EditColleagueDiscount GetDeatails(long Id);
    }
}

using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Application.Contracts.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);

      List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);

        EditColleagueDiscount GetDeatails(long Id);
        OperationResult Remove(long Id);
        OperationResult Restore(long Id);




    }
}

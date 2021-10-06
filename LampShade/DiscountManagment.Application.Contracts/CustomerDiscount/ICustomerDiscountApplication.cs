using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Application.Contracts.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Create(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);

        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search);

        EditCustomerDiscount GetDeatails(long id);

    }
}

using _0_Framework.Domain;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long,CustomerDiscount>
    {

        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search);

        EditCustomerDiscount GetDeatails(long id);

    }
}

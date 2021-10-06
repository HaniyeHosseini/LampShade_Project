using _0_Framework.Application;
using DiscountManagment.Application.Contracts.CustomerDiscount;
using DiscountManagment.Domain.CustomerDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            this.customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Create(DefineCustomerDiscount command)
        {
            var operationresult = new OperationResult();

            if (customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
              return  operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();

            var customerdiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startdate, enddate, command.Reason);
            customerDiscountRepository.Create(customerdiscount);

          return  operationresult.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operationresult = new OperationResult();
           

            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();
            var customerdiscount = customerDiscountRepository.GetBy(command.Id);


            if (customerdiscount == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            if (customerDiscountRepository.Exist(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate &&x.Id != command.Id))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            customerdiscount.Edit(command.ProductId, command.DiscountRate, startdate, enddate, command.Reason);
            customerDiscountRepository.Save();
          return  operationresult.Succedded();
        }

        public EditCustomerDiscount GetDeatails(long id)
        {
            return customerDiscountRepository.GetDeatails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search)
        {
            return customerDiscountRepository.Search(search).ToList();

        }
    }
}

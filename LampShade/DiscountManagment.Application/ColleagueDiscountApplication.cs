using _0_Framework.Application;
using DiscountManagment.Application.Contracts.ColleagueDiscount;
using DiscountManagment.Domain.ColeagueDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueRepository colleagueRepository;

        public ColleagueDiscountApplication(IColleagueRepository colleagueRepository)
        {
            this.colleagueRepository = colleagueRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operationresult = new OperationResult();
            if (colleagueRepository.Exist(x => x.ProductId == command.ProductId))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);
            var colleaguediscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            colleagueRepository.Create(colleaguediscount);
            return operationresult.Succedded();


        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operationresult = new OperationResult();
            var colleagueDiscount = colleagueRepository.GetBy(command.Id);
            if (colleagueRepository.Exist(x => x.Id == command.Id && x.DiscountRate==command.DiscountRate))
                return operationresult.Failed(ApplicationMessages.DuplicatedRecord);

            if (colleagueDiscount == null)
                return operationresult.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            colleagueRepository.Save();
            return operationresult.Succedded();



        }

        public EditColleagueDiscount GetDeatails(long Id)
        {
            return colleagueRepository.GetDeatails(Id);
        }

        public OperationResult Remove(long Id)
        {
            var opearionresult = new OperationResult();


            var discount = colleagueRepository.GetBy(Id);

            if (discount == null)
                return opearionresult.Failed(ApplicationMessages.RecordNotFound);

            else
            {
                discount.Remove();
                colleagueRepository.Save();

            }

            return opearionresult.Succedded();
        }

        public OperationResult Restore(long Id)
        {
            var opearionresult = new OperationResult();


            var discount = colleagueRepository.GetBy(Id);

            if (discount == null)
                return opearionresult.Failed(ApplicationMessages.RecordNotFound);

            else
            {
                discount.Restore();
                colleagueRepository.Save();

            }

            return opearionresult.Succedded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return colleagueRepository.Search(searchModel);
        }
    }
}

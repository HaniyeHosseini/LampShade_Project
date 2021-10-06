using _0_Framework.Application;
using ShopManagment.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagment.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1,1000000,ErrorMessage =ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Range(1,99, ErrorMessage = ValidationMessage.IsRequired)]

        public int DiscountRate { get; set; }

        public List<ProductViewModel> Products { get; set; }

    }
}

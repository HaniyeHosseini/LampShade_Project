using _0_Framework.Application;
using ShopManagment.Application.Contracts.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagment.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Range(1,100000,ErrorMessage =ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]

        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }


}

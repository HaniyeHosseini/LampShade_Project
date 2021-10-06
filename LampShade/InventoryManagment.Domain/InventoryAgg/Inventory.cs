using _0_Framework.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.Domain.InventoryAgg
{
    public class Inventory : EntitiBase
    {
        public long ProdctId { get; private set; }
        public bool InStock { get; private set; }

        public double UnitPrice { get; private set; }

        public List<InventoryOperation> Operations { get; private set; }

        public Inventory()
        {

        }

        public Inventory(long prodctId, double unitPrice)
        {
            ProdctId = prodctId;
            UnitPrice = unitPrice;
            InStock = false;
        }
        public void Edit(long prodctId, double unitPrice)
        {
            ProdctId = prodctId;
            UnitPrice = unitPrice;
            
        }


        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);

            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);

            return plus - minus;
        }


        public void Increase(long count , long operatorid , string description)
        {
            var currentcount = CalculateCurrentCount()+count;
            var operation = new InventoryOperation(true, count, currentcount, operatorid, Id, description, 0);
            this.Operations.Add(operation);
            InStock = currentcount > 0;

        
        }


        public void Reduce(long count, long operatorid, string description, long orderid)
        {
            var currentcount = CalculateCurrentCount() - count;

            var operation = new InventoryOperation(false, count, currentcount, operatorid, Id, description, orderid);

            this.Operations.Add(operation);
            InStock = currentcount > 0;


        }
    }
}

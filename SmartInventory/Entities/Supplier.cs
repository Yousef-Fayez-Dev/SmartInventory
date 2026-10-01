using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Entities
{
    public class Supplier
    {
        public string SupplierName { get; set; }

        public int SupplierID{ get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail{ get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}

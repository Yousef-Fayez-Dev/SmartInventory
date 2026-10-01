using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Entities
{
    public class OrderItem
    {

        //Base Properties
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        //ForeignKey

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        //NavigationProperties
        public virtual Order Order { get; set; } 
        public virtual Product Product { get; set; }


    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Entities
{
    public class Product
    {
        //Base Properties
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int  Quantity { get; set; }

        //NavigationProperties
        public virtual Supplier Supplier { get; set; }
        public virtual Category Category { get; set; }

        //ForeignKey

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        //Lists
        public virtual List <OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

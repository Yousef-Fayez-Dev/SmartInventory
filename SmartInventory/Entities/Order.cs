using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Entities
{
    public class Order
    {
        //Base Properties
        public string OrderName { get; set; } = string.Empty;

        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public int OrderNumber { get; set; }


        //Navigation Property
        public virtual Customer Customer { get; set; }

        //Foreign key

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        //lists
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}

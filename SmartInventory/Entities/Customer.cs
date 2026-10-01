using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Entities
{
    public class Customer
    {
        //Base Properties
        [MaxLength(30)]
        public string CustomerName { get; set; }
        [Key]
        public int CustomerID { get; set; }

        [MaxLength(11)]  [Required]
        public string CustomerPhone { get; set; } = string.Empty;

        [MaxLength(25)]
        public string CustomerEmail { get; set; } = string.Empty;

        [MaxLength(25)]
        public string CustomerCity { get; set; } = string.Empty;

        //Lists
        public virtual List<Order> Orders { get; set; } = new List<Order>();

    }
}

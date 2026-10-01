using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Entities
{
    public class Category
    {
        public string CategoryName { get; set; }
        [Key]
        public int CategoryId { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}

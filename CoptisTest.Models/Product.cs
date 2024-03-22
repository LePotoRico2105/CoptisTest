using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoptisTest.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}

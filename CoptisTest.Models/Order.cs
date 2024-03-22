using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoptisTest.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}

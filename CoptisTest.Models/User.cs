using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CoptisTest.Models
{
    [Index(nameof(Username))]
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Username { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Password { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? LastName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Email { get; set; }

        public List<Access> Accesses { get; set; } = new List<Access>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}

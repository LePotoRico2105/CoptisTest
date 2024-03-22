using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoptisTest.Models
{
    [Index(nameof(Name))]
    public class Access
    {

        [Key]
        public int AccessId { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}

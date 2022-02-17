using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Index("Login")]
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [MaxLength(128), Required]
        public string Name { get; set; } = null!;

        [MaxLength(50), Required]
        public string Password { get; set; } = null!;

        [MaxLength(50), Required]
        public string Login { get; set; } = null!;
    }
}

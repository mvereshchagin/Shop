using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Index("Name")]
    public class Product
    {
        [Key]
        public Guid ID { get; set; }

        [MaxLength(128), Required]
        public string Name { get; set; } = null!;


        [MaxLength(512)]
        public string Description { get; set; } = null!;

        public int? Price { get; set; }
    }
}

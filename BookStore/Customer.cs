using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
    }
}

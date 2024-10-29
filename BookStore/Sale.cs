using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public float AmountPaid { get; set; }
        public float AmountRemain { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Sale()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}

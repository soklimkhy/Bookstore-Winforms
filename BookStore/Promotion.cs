using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace BookStore
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Customer")]  //Certain buyer
        public int ?CustomerId { get; set; }
        public Customer Customer { get; set; }

        [StringLength(255)]
        public string PromotionTitle { get; set; }
        public float DiscountPercentage { get; set; }
        public float PriceAfterDiscount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Ended { get; set; }
    }
}

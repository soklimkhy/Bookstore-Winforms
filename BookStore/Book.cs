using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Author { get; set; }
        [StringLength(255)]
        public string PublishingHouseName { get; set; }
        public int PageCount { get; set; }
        [StringLength(100)]
        public string Genre { get; set; }
        [StringLength(100)]
        public string Sequel { get; set; }
        public float PrimeCost { get; set; }
        public float SalePrice { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }
        public Book()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}

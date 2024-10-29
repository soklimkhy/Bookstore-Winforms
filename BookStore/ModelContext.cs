using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal class ModelContext : DbContext
    {
        public ModelContext() : base("name=BookStoreConnection") { }
        public DbSet<Book> BookList { get; set; }
        public DbSet<Customer> CustomerList { get; set; }
        public DbSet<Sale> SaleList { get; set; }
        public DbSet<Promotion> PromotionList {  get; set; }
        public DbSet<Admin> AdminList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore
{
    public partial class LastCmb : Form
    {
        public LastCmb()
        {
            InitializeComponent();
        }


        private void DashboardPage_Load(object sender, EventArgs e)
        {
            using (ModelContext db = new ModelContext())
            {
                bookBindingSource.DataSource = db.BookList.ToList();
                customerBindingSource.DataSource = db.CustomerList.ToList();
                saleBindingSource.DataSource = db.SaleList.ToList();
                promotionBindingSource.DataSource = db.PromotionList.ToList();

            }
            Book obj = bookBindingSource.Current as Book;
            Customer customer = customerBindingSource.Current as Customer;
            Sale sale = saleBindingSource.Current as Sale;
            Promotion promotion = promotionBindingSource.Current as Promotion;


        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            
            bookBindingSource.Add(new Book());
            bookBindingSource.MoveLast();
            TitleTxb.Focus();

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            TitleTxb.Focus();
            Book obj = bookBindingSource.Current as Book;
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            bookBindingSource.ResetBindings(false);
            DashboardPage_Load(sender, e);

        }

        private void dataGridView1_CellClick(Object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (ModelContext db = new ModelContext())
                {
                    Book obj = bookBindingSource.Current as Book;
                    if (obj != null)
                    {
                        if (db.Entry(obj).State == EntityState.Detached)
                            db.Set<Book>().Attach(obj);

                        db.Entry(obj).State = EntityState.Deleted;
                        db.SaveChanges();
                        MessageBox.Show("Delete Successfully");
                        bookBindingSource.RemoveCurrent();
                    }
                }
            }
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
       
            if (string.IsNullOrWhiteSpace(TitleTxb.Text) ||
                string.IsNullOrWhiteSpace(AuthorTxb.Text) ||
                string.IsNullOrWhiteSpace(PublishingHouseNameTxb.Text) ||
                !int.TryParse(PageCountTxb.Text, out _) ||
                !float.TryParse(PrimeCostTxb.Text, out _) ||
                !float.TryParse(SalePriceTxb.Text, out _) ||
                string.IsNullOrWhiteSpace(GenreCmb.Text) ||
                string.IsNullOrWhiteSpace(SequelCmb.Text) ||
                !DateTime.TryParse(DatePublishedDtp.Text, out _))
            {
                MessageBox.Show("Please fill in all required fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (ModelContext db = new ModelContext())
            {
                Book obj = bookBindingSource.Current as Book;
                if (obj != null)
                {
                    obj.Updated = DateTime.Now;
                    if (db.Entry<Book>(obj).State == EntityState.Detached)
                        db.Set<Book>().Attach(obj);
                    if (obj.BookId == 0)
                        db.Entry<Book>(obj).State = EntityState.Added;
                    else
                        db.Entry<Book>(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Save Successfully");
                    dataGridView1.Refresh();
                }
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchText = SearchTxb.Text.ToLower();
            using (ModelContext db = new ModelContext())
            {
                var result = db.BookList
                    .Where(b => b.Title.ToLower().Contains(searchText)
                             || b.Author.ToLower().Contains(searchText)
                             || b.Genre.ToLower().Contains(searchText))
                    .ToList();

                // Update the data source and refresh the DataGridView
                bookBindingSource.DataSource = result;
                dataGridView1.Refresh();
            }
        }

        private void SortBooks()
        {
            string sortOrder = SortingCmb.SelectedItem.ToString().ToLower();
            using (ModelContext db = new ModelContext())
            {
                var sortedBooks = sortOrder == "ascending"
                    ? db.BookList.OrderBy(b => b.BookId).ToList()
                    : db.BookList.OrderByDescending(b => b.BookId).ToList();

                // Update the data source and refresh the DataGridView
                bookBindingSource.DataSource = sortedBooks;
                dataGridView1.Refresh();
            }
        }

        private void SortingCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortBooks();
        }

        private void BestOfCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBestOf();
        }
        private void LoadBestOf()
        {
            string selectedOption = BestOfCmb.SelectedItem.ToString();
            using (ModelContext db = new ModelContext())
            {
                switch (selectedOption)
                {
                    case "Default":
                        LoadDefault(db);
                        break;
                    case "Best Sellers":
                        LoadBestSellers(db);
                        break;
                    case "Popular Authors":
                        LoadPopularAuthors(db);
                        break;
                    case "Popular Genres":
                        LoadPopularGenres(db);
                        break;
                }
            }
        }
        private void LoadDefault(ModelContext db)
        {
            var allBooks = db.BookList.ToList();
            dataGridView1.DataSource = allBooks;
        }
        private void LoadBestSellers(ModelContext db)
        {
            // Placeholder logic for Best Sellers - adjust as needed
            var bestSellers = db.SaleList
                .GroupBy(s => s.BookId)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .Select(g => g.FirstOrDefault().Book)
                .ToList();

            dataGridView1.DataSource = bestSellers;
        }

        private void LoadPopularAuthors(ModelContext db)
        {
            // Placeholder logic for Popular Authors - adjust as needed
            var popularAuthors = db.BookList
                .GroupBy(b => b.Author)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .Select(g => g.Key)
                .ToList();

            dataGridView1.DataSource = popularAuthors
                .Select(author => new { Author = author })
                .ToList();
        }

        private void LoadPopularGenres(ModelContext db)
        {
            // Placeholder logic for Popular Genres - adjust as needed
            var popularGenres = db.BookList
                .GroupBy(b => b.Genre)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .Select(g => g.Key)
                .ToList();

            dataGridView1.DataSource = popularGenres
                .Select(genre => new { Genre = genre })
                .ToList();
        }

        private void AddProBtn_Click(object sender, EventArgs e)
        {
            promotionBindingSource.Add(new Promotion());
            promotionBindingSource.MoveLast();
           

        }

        private void EditProBtn_Click(object sender, EventArgs e)
        {
            PromotionTitle.Focus();
            Promotion obj = promotionBindingSource.Current as Promotion;
        }

        private void RefreshProBtn_Click(object sender, EventArgs e)
        {
            promotionBindingSource.ResetBindings(false);
            DashboardPage_Load(sender, e);
        }

        private void SaveProBtn_Click(object sender, EventArgs e)
        {
            // Perform validations
            if (string.IsNullOrWhiteSpace(BookIdPro.Text) ||
                string.IsNullOrWhiteSpace(CustomerIdPro.Text) ||
                string.IsNullOrWhiteSpace(PromotionTitle.Text) ||
                !float.TryParse(PromotionPer.Text, out float discountPercentage) ||
                !DateTime.TryParse(StartDtp.Text, out DateTime startDate) ||
                !DateTime.TryParse(EndDtp.Text, out DateTime endDate))
            {
                MessageBox.Show("Please fill in all required fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (ModelContext db = new ModelContext())
            {
                // Retrieve the book using BookId
                int bookId = int.Parse(BookIdPro.Text);
                int customerId = int.Parse(CustomerIdPro.Text);
                var book = db.BookList.FirstOrDefault(b => b.BookId == bookId);
                var customer = db.CustomerList.FirstOrDefault(c => c.CustomerId == customerId);

                if (book == null || customer == null)
                {
                    MessageBox.Show("Book or Buyer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calculate PriceAfterDiscount
                float priceAfterDiscount = book.SalePrice * (1 - discountPercentage / 100);
                Promotion obj = promotionBindingSource.Current as Promotion;
                if (obj != null)
                {
                    obj.BookId = bookId;
                    obj.CustomerId = customerId;
                    obj.PromotionTitle = PromotionTitle.Text;
                    obj.DiscountPercentage = discountPercentage;
                    obj.PriceAfterDiscount = priceAfterDiscount;
                    obj.Created = DateTime.Now;
                    obj.Ended = endDate;

                    db.PromotionList.AddOrUpdate(obj);


                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Save Successfully");
                        dataGridView4.Refresh();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }






        private void DeleteProBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (ModelContext db = new ModelContext())
                {
                    Promotion obj = promotionBindingSource.Current as Promotion;
                    if (obj != null)
                    {
                        if (db.Entry(obj).State == EntityState.Detached)
                            db.Set<Promotion>().Attach(obj);

                        db.Entry(obj).State = EntityState.Deleted;
                        db.SaveChanges();
                        MessageBox.Show("Delete Successfully");
                        promotionBindingSource.RemoveCurrent();
                    }
                }
            }
        }
    }



}

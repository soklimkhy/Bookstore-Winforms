using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTxb.Text;
            string password = PasswordTxb.Text;

            using (ModelContext db = new ModelContext())
            {
                var admin = db.AdminList.FirstOrDefault(a => a.Username == username && a.Password == password);
                if (admin != null)
                {
                    LastCmb LastCmb = new LastCmb();
                    LastCmb.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Access = Microsoft.Office.Interop.Access;

namespace LNB
{
    public partial class LogInPage : Form
    {
        private Access.Application acApp;
        private string dbpath = @"C:\Users\gambe\Documents\LNB_Airlines.accdb";
        public LogInPage()
        {
            InitializeComponent();

            acApp = new Access.Application();
            acApp.OpenCurrentDatabase(dbpath, false);
        }

        private bool ValidateUser(string username, string passwords)
        {
            var db = acApp.CurrentDb();
            var query = $"SELECT COUNT(*) AS EmployeeCount FROM Employees WHERE Username = '{username}' AND Password = '{passwords}'";
            var record = db.OpenRecordset(query);
            bool isValid = false;
            if (!record.EOF)
            {
                isValid = (record.Fields["EmployeeCount"].Value > 0);
            }
            record.Close();
            return isValid;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new SignUpPage().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateUser(txtUsername.Text, txtPassword.Text))
            {
                new Dashboard().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
                txtPassword.Clear();
                txtUsername.Clear();
                txtUsername.Focus();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
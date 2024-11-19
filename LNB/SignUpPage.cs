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
    public partial class SignUpPage : Form
    {
        private Access.Application acApp;
        private string dbpath = @"C:\Users\gambe\Documents\LNB_Airlines.accdb";

        public SignUpPage()
        {
            InitializeComponent();

            acApp = new Access.Application();
            acApp.OpenCurrentDatabase(dbpath, false);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new LogInPage().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            try
            {
                var username = txtNewUsername.Text;
                var password = txtNewPassword.Text;

                string query = $"INSERT INTO Employees (Username, Password) VALUES ('{username}', '{password}')";
                acApp.DoCmd.RunSQL(query);

                MessageBox.Show("User registered successfully! Please go back and log in.");
                new LogInPage().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to register:  " + ex.Message);
            }
        }
        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            if (acApp != null)
            {
                acApp.Quit();
                acApp=null;
            }
        }
    }
}


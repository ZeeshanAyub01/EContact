using EContact.EcontactClasses;
using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }

        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;

        ContactClass c = new ContactClass();
        private void EContact_Load(object sender, EventArgs e)
        {
           conn = new NpgsqlConnection("Server=\"localhost\";Port=5432;Database=test;User Id=postgres;Password=progStuff;");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get value from the input fields
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNo.Text;
            c.Address = txtAddress.Text;
            c.Gender = comboBox1.Text;
            bool success = false;
            try
            {
                success = c.Insert(c);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted");
            }
            else
            {
                MessageBox.Show("Failed to add new contact. Try Again");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void db_test_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            sql = @"INSERT INTO Items (item_id, item_name)
VALUES (3, 'Eggs');";
            cmd = new NpgsqlCommand(sql, conn);
        
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine("=> " + i);

            conn.Close();
        }
    }
}

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
using EContact.EcontactClasses;

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
        DataTable dt;
        ContactClass c = new ContactClass();
        private void EContact_Load(object sender, EventArgs e)
        {
            //conn = new NpgsqlConnection("Server=\"localhost\";Port=5432;Database=test;User Id=postgres;Password=progStuff;");
            //conn = new NpgsqlConnection(myconnstrng);
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
            sql = @"SELECT * FROM Items;";
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            //int i = cmd.ExecuteNonQuery();
            //Console.WriteLine("=> " + i);
            conn.Close();
            dgv1.DataSource = null;
            dgv1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dt = c.Select();
                dgv1.DataSource = null;
                dgv1.DataSource = dt;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }


        }
    }
}

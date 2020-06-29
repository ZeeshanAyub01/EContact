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
            dt = c.Select();
            dgv1.DataSource = null;
            dgv1.DataSource = dt;
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
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add new contact. Try Again");
            }

            dt = c.Select();
            dgv1.DataSource = null;
            dgv1.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtContactID.Text);
            bool success = c.Delete(c);

            if (success == true)
            {
                MessageBox.Show("Contact has been successfully deleted!");
                dt = c.Select();
                dgv1.DataSource = null;
                dgv1.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete contact! Try Again!");
            }

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

        private void button2_Click(object sender, EventArgs e)//View all contacts
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtContactID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
            comboBox1.Text = "";
        }

        private void dgv1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get all the data from the row clicked in the data grid view into the text fields
            int rowIndex = e.RowIndex;
            txtContactID.Text = dgv1.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgv1.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgv1.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNo.Text = dgv1.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dgv1.Rows[rowIndex].Cells[4].Value.ToString();
            comboBox1.Text = dgv1.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtContactID.Text);
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNo.Text;
            c.Address = txtAddress.Text;
            c.Gender = comboBox1.Text;

            bool success = c.update(c);

            if(success == true)
            {
                MessageBox.Show("Contact has been successfully updated!");
                dt = c.Select();
                dgv1.DataSource = null;
                dgv1.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact! Try Again!");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

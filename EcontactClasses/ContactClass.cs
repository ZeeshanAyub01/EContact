using System;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact.EcontactClasses
{
    class ContactClass
    {
        //Getter Setter Properties
        //Acts as a data carrier in our application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private string myconnstrng = "Server=\"localhost\";Port=5432;Database=EContact;User Id=postgres;Password=progStuff;";
        //private NpgsqlConnection conn = new NpgsqlConnection("Server=\"localhost\";Port=5432;Database=test;User Id=postgres;Password=progStuff;");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        public DataTable Select()
        {
            //SqlConnection conn = new SqlConnection(myconnstrng);
            conn = new NpgsqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Writing ou SQL query
                sql = @"SELECT * FROM ContactsInfo";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                conn.Open();
                dt.Load(cmd.ExecuteReader());
                //int i = cmd.ExecuteNonQuery();
                //Console.WriteLine("=> " + i);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public bool Insert(ContactClass c)
        {
            bool isSuccess = false;
            int numRows = 0;
            conn = new NpgsqlConnection(myconnstrng);

            try
            {
                sql = "INSERT INTO ContactsInfo (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                Console.WriteLine("=> " + c.FirstName);
                Console.WriteLine("=> " + c.LastName);
                Console.WriteLine("=> " + c.ContactNo);
                Console.WriteLine("=> " + c.Address);
                Console.WriteLine("=> " + c.Gender);
                conn.Open();
                numRows = cmd.ExecuteNonQuery();
                //int rows = cmd.ExecuteNonQuery();

                if (numRows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

    }
}

using System;
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

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public DataTable Select()
        {
            //SqlConnection conn = new SqlConnection(myconnstrng);
            
            DataTable dt = new DataTable();
            try
            {
                //Writing ou SQL query
                string sql = "SELECT * FROM Contacts_Info";
                //SqlCommand cmd = new SqlCommand(sql, conn);
                OdbcCommand cmd = new OdbcCommand(sql);
                using (OdbcConnection connection = new OdbcConnection(myconnstrng))
                {
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    // The connection is automatically closed at
                    // the end of the Using block.
                }
                //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //conn.Open();
                //adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //conn.Close();
            }
            return dt;
        }

        public bool Insert(ContactClass c)
        {
            bool isSuccess = false;
            int numRows = 0;
            OdbcConnection connection = new OdbcConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO Contacts_Info (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //SqlCommand cmd = new SqlCommand(sql, conn);
                OdbcCommand cmd = new OdbcCommand(sql);

                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                cmd.Connection = connection;
                connection.Open();
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

            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }

}
}

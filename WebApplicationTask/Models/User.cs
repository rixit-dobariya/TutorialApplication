using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebApplicationTask.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        static SqlConnection con { get; set; }
        public bool Login()
        {
            bool isValid = false;
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01\source\repos\TutorialApplication\WebApplicationTask\App_Data\TaskDB.mdf;Integrated Security=True");
            con.Open();
            string query = $"select * from Users where Email='{Email}' and Password='{Password}'";
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query,con);
            dataAdapter.Fill(dt);
            isValid = dt.Rows.Count > 0;
            con.Close();

            return isValid;
        }
    }
}

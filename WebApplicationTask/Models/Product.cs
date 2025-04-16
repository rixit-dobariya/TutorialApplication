using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplicationTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        static SqlConnection con { get; set; }
        public static List<Product> GetAll()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01\source\repos\TutorialApplication\WebApplicationTask\App_Data\TaskDB.mdf;Integrated Security=True");
            con.Open();
            List<Product> products = new List<Product>();
            string query = $"select * from Product";
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
            dataAdapter.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                products.Add(new Product { Id = (int)dr["Id"], ProductName = (string)dr["ProductName"], Price = (decimal)dr["Price"], Quantity = (int)dr["Quantity"]  });
            }
            con.Close();

            return products;
        }
    }
}

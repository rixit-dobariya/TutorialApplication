using Microsoft.Data.SqlClient;
using System.Data;

namespace WorkshopWork.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }
        static SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WorkshopStudent;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        static Employee(){ con.Open(); }
        public List<Employee> GetData(string id="")
        {
            List<Employee> employees = new List<Employee>();
            string query = "select * from tbl_emp";
            if (!id.Equals(""))
            {
                query += $" where Id={id}";
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                employees.Add(new Employee {
                    Id = Convert.ToInt32(dr["Id"].ToString()),
                    Name = dr["Name"].ToString(),
                    Dept = dr["Dept"].ToString(),
                    Salary = Convert.ToInt32(dr["Salary"].ToString())
                });
            }

            return employees;
        }
    }
}

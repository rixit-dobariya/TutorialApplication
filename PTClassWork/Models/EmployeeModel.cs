using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PTClassWork.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your department name")]
        public string Dept { get; set; }
        [Required(ErrorMessage = "Enter your salary")]
        public int Salary { get; set; }

        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01\source\repos\TutorialApplication\PTClassWork\App_Data\PTClassWorkDB.mdf;Integrated Security=True";
        SqlConnection con = new SqlConnection(connectionString);

        public List<EmployeeModel> GetAll()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            DataTable dataTable = new DataTable();
            string query = "select * from tbl_emp";
            con.Open();
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con))
            {
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    employeeList.Add(new EmployeeModel
                    {
                        Id = (int)row["Id"],
                        Name = (string)row["Name"],
                        Dept = (string)row["Dept"],
                        Salary = (int)row["Salary"]
                    });
                }
            }
            con.Close();
            return employeeList;
        }
        public EmployeeModel Get(int id)
        {
            EmployeeModel employee = new EmployeeModel();
            string query = "select * from tbl_emp where Id=@id";
            
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader()) 
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        employee = new EmployeeModel()
                        {
                            Id=(int)dataReader["Id"],
                            Name=(string)dataReader["Name"],
                            Dept=(string)dataReader["Dept"],
                            Salary=(int)dataReader["Salary"]
                        };
                    }
                }
                con.Close();
            }
            
            return employee;
        }
        public bool Add(EmployeeModel employee)
        {
            bool success = false;
            if (employee == null)
            {
                return success;
            }
            string query = @"insert into tbl_emp(Name, Dept, Salary) 
                        values(@name, @dept, @salary)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("name", employee.Name);
                cmd.Parameters.AddWithValue("dept", employee.Dept);
                cmd.Parameters.AddWithValue("salary", employee.Salary);

                con.Open();
                success = cmd.ExecuteNonQuery() >= 1;
                con.Close();
            }
            return success;
        }
        public bool Update(EmployeeModel employee)
        {
            bool success = false;
            if (employee == null)
            {
                return success;
            }
            string query = @"update tbl_emp set Name=@name, Dept=@dept, Salary=@salary where Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("name", employee.Name);
                cmd.Parameters.AddWithValue("dept", employee.Dept);
                cmd.Parameters.AddWithValue("salary", employee.Salary);
                cmd.Parameters.AddWithValue("id", employee.Id);

                con.Open();
                success = cmd.ExecuteNonQuery() >= 1;
                con.Close();
            }
            return success;
        }
        public bool Delete(int id)
        {
            bool success = false;
            if (id == 0)
            {
                return success;
            }
            string query = @"delete from tbl_emp where Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("id", id);

                con.Open();
                success = cmd.ExecuteNonQuery() >= 1;
                con.Close();
            }
            return success;
        }
    }
}

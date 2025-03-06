using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WorkshopWork.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }



        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WorkshopStudent;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

       
        public bool Insert(Student student)
        {
            if(!(string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(student.Email) && string.IsNullOrEmpty(student.Address) && string.IsNullOrEmpty(student.Age.ToString())))
            {
                SqlCommand cmd = new SqlCommand("insert into Students values(@Name, @Email, @Age, @Address)",con);
                cmd.Parameters.AddWithValue("Name", student.Name);
                cmd.Parameters.AddWithValue("Email", student.Email);
                cmd.Parameters.AddWithValue("Address", student.Address);
                cmd.Parameters.AddWithValue("Age", student.Age);
                con.Open();
                return cmd.ExecuteNonQuery() >= 1;
            }
            return false;
        }

        public bool Update(Student student)
        {
            if (!(string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(student.Email) && string.IsNullOrEmpty(student.Address) && string.IsNullOrEmpty(student.Age.ToString())))
            {
                SqlCommand cmd = new SqlCommand("update Students set Name=@Name, Email=@Email, Age=@Age, Address=@Address where Id=@Id",con);
                cmd.Parameters.AddWithValue("Name", student.Name);
                cmd.Parameters.AddWithValue("Email", student.Email);
                cmd.Parameters.AddWithValue("Address", student.Address);
                cmd.Parameters.AddWithValue("Age", student.Age);
                cmd.Parameters.AddWithValue("Id", student.Id);
                con.Open();
                return cmd.ExecuteNonQuery() >= 1;
            }
            return false;
        }
        public bool Delete(Student student)
        {
            if (!string.IsNullOrEmpty(student.Id.ToString()))
            {
                SqlCommand cmd = new SqlCommand("delete from Students where Id=@Id", con);
                cmd.Parameters.AddWithValue("Id", student.Id);
                con.Open();
                return cmd.ExecuteNonQuery() >= 1;
            }
            return false;
        }
        public List<Student> getData(string id)
        {
            List<Student> studentsList = new List<Student>();
            string query = "select * from Students";
            if (!string.IsNullOrEmpty(id))
            {
                query = "select * from Students where Id=" + id;
            }
            SqlDataAdapter apt = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            apt.Fill(ds);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                studentsList.Add(new Student {
                    Id = Convert.ToInt32(dr["Id"].ToString()),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Address = dr["Address"].ToString(),
                    Age = Convert.ToInt32(dr["Age"].ToString())
                });
            }
            return studentsList;
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Data;

namespace PTClassWork.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int Sem { get; set; }
        public string Branch { get; set; }
        public string[] Hobby {get;set;}
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\01\\source\\repos\\TutorialApplication\\PTClassWork\\App_Data\\PTClassWorkDB.mdf;Integrated Security=True");
        public bool Create()
        {
            bool result;
            using (SqlCommand cmd = new SqlCommand("insert into tbl_stud2(Name, Email, Age,Sem, Branch) values(@name, @email, @age, @sem, @branch)",con))
            {
                cmd.Parameters.AddWithValue("name",Name);
                cmd.Parameters.AddWithValue("email", Email);
                cmd.Parameters.AddWithValue("age", Age);
                cmd.Parameters.AddWithValue("sem", Sem);
                cmd.Parameters.AddWithValue("branch", Branch);
                con.Open();
                result = cmd.ExecuteNonQuery()>=1;
            }
            con.Close();
            return result;
        }
        public bool Update()
        {
            bool result;
            using (SqlCommand cmd = new SqlCommand("update tbl_stud2 set Name=@name, Email=@email, Age=@age,Sem=@sem,Branch=@branch where Id=@id", con))
            {
                cmd.Parameters.AddWithValue("name", Name);
                cmd.Parameters.AddWithValue("email", Email);
                cmd.Parameters.AddWithValue("age", Age);
                cmd.Parameters.AddWithValue("sem", Sem);
                cmd.Parameters.AddWithValue("branch", Branch);
                cmd.Parameters.AddWithValue("id", Id);
                con.Open();
                result = cmd.ExecuteNonQuery() >= 1;
            }
            con.Close();
            return result;
        }
        public bool Delete()
        {
            bool result;
            using (SqlCommand cmd = new SqlCommand("delete from tbl_stud2 where Id=@id", con))
            {
                cmd.Parameters.AddWithValue("id", Id);
                con.Open();
                result = cmd.ExecuteNonQuery() >= 1;
            }
            con.Close();
            return result;
        }
        public List<Student> GetStudents(int? id)
        {
            List<Student> students = new List<Student>();
            DataSet ds = new DataSet();
            string query = "select * from tbl_stud2";
            if (id!=null)
            {
                query = "select * from tbl_stud2 where Id = " + id;
            }
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
            {
                con.Open();
                adapter.Fill(ds);
                con.Close();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    students.Add(new Student
                    {
                        Id = (int)dr["Id"],
                        Name= (string)dr["Name"],
                        Email = (string)dr["Email"],
                        Age = (int)dr["Age"],
                        Sem = (int)dr["Sem"],
                        Branch = (string)dr["Branch"]
                    });
                }
            }
                return students;
        }
    }
}

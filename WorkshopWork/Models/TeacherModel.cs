using Microsoft.Data.SqlClient;
using System.Data;

namespace WorkshopWork.Models
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public double Salary { get; set; }
        public int Experience { get; set; }
        public int Semester { get; set; }

        public static string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01\source\repos\TutorialApplication\WorkshopWork\App_Data\WorkshopDB.mdf;Integrated Security=True";

        private SqlConnection con= new SqlConnection(conString);
        
        public bool Create()
        {
            bool result;
            string query = "insert into Teachers values(@name, @subject, @salary, @experience, @semester)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name",Name);
                cmd.Parameters.AddWithValue("@subject", Subject);
                cmd.Parameters.AddWithValue("@salary", Salary);
                cmd.Parameters.AddWithValue("@experience", Experience);
                cmd.Parameters.AddWithValue("@semester", Semester);

                con.Open();
                result = cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return result;
        }
        public bool Update()
        {
            bool result;
            string query = "update Teachers set TeacherName=@name, Subject=@subject, Salary=@salary, Experience =@experience, Semester=@semester where Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@subject", Subject);
                cmd.Parameters.AddWithValue("@salary", Salary);
                cmd.Parameters.AddWithValue("@experience", Experience);
                cmd.Parameters.AddWithValue("@semester", Semester);
                cmd.Parameters.AddWithValue("@id", Id);

                con.Open();
                result = cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return result;
        }
        public bool Delete()
        {
            bool result;
            string query = "delete from Teachers where Id=@id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", Id);
                con.Open();
                result = cmd.ExecuteNonQuery() > 0;
                con.Close();
            }
            return result;
        }
        public List<TeacherModel> GetAll()
        {
            List<TeacherModel> teachers = new List<TeacherModel>();
            string query = "select * from Teachers";
            using (SqlDataAdapter da = new SqlDataAdapter(query,con))
            {
                con.Open();
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    teachers.Add(new TeacherModel
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["TeacherName"],
                        Experience = (int)dr["Experience"],
                        Subject = (string)dr["Subject"],
                        Salary = Convert.ToDouble(dr["Salary"].ToString()),
                        Semester = (int)dr["Semester"]
                    });
                }
                con.Close();
            }
            return teachers;
        }
        public TeacherModel Get(int teacherId)
        {
            string query = "select * from Teachers where Id=@id";
            using (SqlCommand cmd = new SqlCommand(query,con))
            {
                cmd.Parameters.AddWithValue("@id", teacherId);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        Id = teacherId;
                        Name = (string)dr["TeacherName"];
                        Subject = (string)dr["Subject"];
                        Salary = Double.Parse(dr["Salary"].ToString());
                        Experience = (int)dr["Experience"];
                        Semester = (int)dr["Semester"];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            con.Close();
            return this;
        }
        
    }
}

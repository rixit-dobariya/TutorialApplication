using System.ComponentModel.DataAnnotations;

namespace PracticeApplication.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Student name must not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Student class name must not be empty")]
        public string ClassName{ get; set; }
        [Required(ErrorMessage = "Percentage must not be empty")]
        [Range(0,100,ErrorMessage ="Percentage must be between 0 to 100")]
        public double percentage { get; set; }
    }
}

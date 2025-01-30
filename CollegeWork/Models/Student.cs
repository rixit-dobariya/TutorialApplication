using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CollegeWork.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required, NotNull]
        public string Name { get; set; }
        [Required, NotNull]
        public string Branch { get; set; }
        [Required]
        [DisplayName("Roll No")]
        public string RollNo { get; set; }
    }
}

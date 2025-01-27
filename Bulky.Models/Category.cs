using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TutorialApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [DisplayName("Display Order")]
        [Required]
        [Range(1, 100, ErrorMessage="Display order must be between 0 and 100")]
        public int DisplayOrder { get; set; }
    }
}

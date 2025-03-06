using System.ComponentModel.DataAnnotations;

namespace WorkshopApi.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }   
        public string status { get; set; }
        public int rating { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Assignment_3_Resume_Builder.Models
{
    public class Comment
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;



    }
}

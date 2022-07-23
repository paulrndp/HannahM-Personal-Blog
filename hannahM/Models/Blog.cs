using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is a required field and can't be empty.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Content is a required field and can't be empty.")]
        public string? Content { get; set; }
        public string? Status { get; set; }
        public int? Views { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string? Slug =>
            Title?.Replace(" ","-").ToLower();
    }   
}

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
        public string? BTitle { get; set; }
        [Required(ErrorMessage = "Content is a required field and can't be empty.")]
        public string? BContent { get; set; }
        public string? BStatus { get; set; }
        public int? BViews { get; set; }
        public DateTime BCreatedDateTime { get; set; } = DateTime.Now;

        public string? Slug =>
            BTitle?.Replace(" ","-").ToLower();
    }   
}

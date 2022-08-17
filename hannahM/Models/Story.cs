using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class Story
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Desc { get; set; }
        public string? Tags { get; set; }
        public string? Genre { get; set; }
        public byte[]? Cover { get; set; }
        public int? Views { get; set; }
        public int? TotalChapters { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string? Slug =>
            Title?.Replace(" ","-").ToLower();
    }   
}

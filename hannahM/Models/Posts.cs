using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public int? Views { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string? Slug =>
            Title?.Replace(" ", "-").ToLower();
    }
}

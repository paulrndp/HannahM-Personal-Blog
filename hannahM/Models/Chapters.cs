using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class Chapters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int story_id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string? temp { get; set; }
        public string? Slug =>
         Title?.Replace(" ", "-").ToLower();
    }   
}

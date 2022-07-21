using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class RandomThoughts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? RTitle { get; set; }
        [Required]
        public string? RContent { get; set; }
        public string? RStatus { get; set; }
        public int? RViews { get; set; }
        public DateTime RCreatedDateTime { get; set; } = DateTime.Now;

        public string? Slug=>
            RTitle?.Replace(" ","-").ToLower();
    }
}

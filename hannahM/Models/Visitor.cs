using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class Visitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int postId { get; set; }

        public string? Point { get; set; }

        public string? visitorIp { get; set; }
    }
}

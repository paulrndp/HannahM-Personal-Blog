using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hannahM.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your username.")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }        
        
        [Required(ErrorMessage = "First name field is required.")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last name field is requried.")]
        public string? LastName { get; set; }

        public string? Desc { get; set; }

        public byte[]? Profile { get; set; }


        public string? Slug => Firstname?.Replace(" ", "-").ToLower();
    }
}

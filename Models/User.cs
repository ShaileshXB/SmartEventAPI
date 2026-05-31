using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEventAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        [Column(TypeName = "varchar(100)")]
        public string? Password { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string? Role { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }

    public class RegisterDTO
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
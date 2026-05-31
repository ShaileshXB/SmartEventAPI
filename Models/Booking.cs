using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEventAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [Range(1, 10)]
        public int Seats { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
    }
    public class BookingDTO
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Seats { get; set; }
    }
}
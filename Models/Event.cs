using System.ComponentModel.DataAnnotations;

namespace SmartEventAPI.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string? EventName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string? Location { get; set; }

        [Required]
        [StringLength(250)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Price must be positive")]
        public int Price { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Seats must be greater than 0")]
        public int Seat { get; set; }

        // Navigation Property
        public ICollection<Booking>? Bookings { get; set; }
    }

    public class EventDTO
    {
        [Required]
        [StringLength(100)]
        public string? EventName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string? Location { get; set; }

        [Required]
        [StringLength(250)]
        public string? Description { get; set; }

        [Range(0, 100000)]
        public int Price { get; set; }

        [Range(1, 1000)]
        public int Seat { get; set; }
    }
}
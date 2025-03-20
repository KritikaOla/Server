using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime DateOfPurchase { get; set; } = DateTime.UtcNow;
    }
}

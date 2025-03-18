
namespace LibraryManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; } // Changed from string to int
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
    }
}


using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        // ✅ Get all books
        public async Task<List<Book>> GetAsync()
        {
            return await _context.Books.ToListAsync();
        }

        // ✅ Get book by Id
        public async Task<Book?> GetAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        // ✅ Create new book
        public async Task CreateAsync(Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
        }

        // ✅ Update existing book
        public async Task UpdateAsync(int id, Book updatedBook)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null) return;

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.PublishedYear = updatedBook.PublishedYear;

            await _context.SaveChangesAsync();
        }

        // ✅ Delete book
        public async Task RemoveAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}

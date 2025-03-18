using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // ✅ PUBLIC: Anyone can view books
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Book>>> Get()
        {
            var books = await _bookService.GetAsync();
            return Ok(books);
        }

        // ✅ PUBLIC: Anyone can view a single book by ID
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // ✅ PROTECTED: Only Admin can add books
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> Post([FromBody] Book newBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        // ✅ PROTECTED: Only Admin can update books
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Book updatedBook)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookService.GetAsync(id);

            if (book == null)
                return NotFound();

            updatedBook.Id = id; // Ensure the ID stays consistent

            await _bookService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        // ✅ PROTECTED: Only Admin can delete books
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
                return NotFound();

            await _bookService.RemoveAsync(id);

            return NoContent();
        }
    }
}

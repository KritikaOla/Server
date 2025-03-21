using Microsoft.AspNetCore.Mvc;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using System;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly LibraryDbContext _context;

		//  Constructor with debug log
		public ContactController(LibraryDbContext context)
		{
			_context = context;
			Console.WriteLine("ContactController instantiated");
		}

		// Basic ping endpoint to test routing
		[HttpGet("ping")]
		public IActionResult Ping()
		{
			Console.WriteLine(" Ping endpoint hit");
			return Ok("Ping successful");
		}

		//  POST endpoint
		[HttpPost]
		public async Task<IActionResult> PostContact([FromBody] Contact contact)
		{
			if (contact == null)
			{
				return BadRequest("Invalid contact data.");
			}

			contact.CreatedAt = DateTime.Now;

			_context.Contacts.Add(contact);
			await _context.SaveChangesAsync();

			return Ok(new { message = "Contact message saved successfully!" });
		}
	}
}

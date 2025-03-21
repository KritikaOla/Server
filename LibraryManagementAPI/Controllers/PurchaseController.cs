using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication
    public class PurchaseController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public PurchaseController(LibraryDbContext context)
        {
            _context = context;
            Console.WriteLine("✅ PurchaseController initialized.");
        }

        //  GET: api/Purchase/user-orders (Get orders for the logged-in user)
        [HttpGet("user-orders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                Console.WriteLine("❌ Unauthorized access attempt.");
                return Unauthorized("User not authenticated.");
            }

            Console.WriteLine($"📢 Fetching orders for: {userEmail}");

            var orders = await _context.Purchases
                .Where(p => p.Email == userEmail)
                .ToListAsync();

            if (!orders.Any())
            {
                Console.WriteLine("⚠ No orders found for this user.");
                return NotFound("No orders found.");
            }

            Console.WriteLine($"✅ {orders.Count} orders found for {userEmail}.");
            return Ok(orders);
        }

        [HttpPost]
        [Route("make-purchase")] // Explicitly define the route for clarity
        public async Task<IActionResult> MakePurchase([FromBody] List<Purchase> purchases)
        {
            Console.WriteLine("🛒 MakePurchase API called!");

            if (purchases == null || purchases.Count == 0)
            {
                Console.WriteLine("❌ Received empty purchase list.");
                return BadRequest("Invalid purchase data.");
            }

            try
            {
                _context.Purchases.AddRange(purchases); //  Save multiple purchases
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ {purchases.Count} purchases saved successfully!");
                return Ok(new { message = "✅ Purchase successful!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return StatusCode(500, $"❌ Internal Server Error: {ex.Message}");
            }
        }
    }
}

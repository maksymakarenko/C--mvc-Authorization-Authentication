using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAuthenticationAuthorization.Models;

namespace TaskAuthenticationAuthorization.Controllers
{
    [Authorize(Policy = "Bosoms")]
    public class DiscountController : Controller
    {
        private readonly ShoppingContext _context;

        public DiscountController(ShoppingContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> MyDiscount()
        {
            var currentUser = await _context.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            return View(currentUser);
        }
    }
}
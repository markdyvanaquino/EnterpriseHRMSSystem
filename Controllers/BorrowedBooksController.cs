using LibraryManagement.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BorrowedBooks : Controller
    {
        private readonly ApplicationDbContext _context;
        public BorrowedBooks(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BorrowedBooksPage()
        {
            var borrowedBooks = await _context.Transactions
                .Where(transaction => transaction.Status == "Borrowed")
                .ToListAsync();

            return View(borrowedBooks);

        }
    }
}

using LibraryManagement.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookTransactionsController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BookTransactionsPage() 
        {
            var transactions = await _context.Transactions
                .OrderByDescending(transactions => transactions.BorrowDate)
                .ToListAsync();

            return View(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooks(string? query)
        {
            var bookSearchedCollection = await _context.Transactions
                .Where(transaction => transaction.TransactionID.ToString() == query || transaction.UserID == query || transaction.BookISBN == query || transaction.BorrowerName == query)
                .ToListAsync();

            return View("BookTransactionsPage", bookSearchedCollection);
        }
    }
}

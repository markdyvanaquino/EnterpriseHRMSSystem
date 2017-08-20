using LibraryManagement.Areas.Identity.Data;
using LibraryManagement.Database;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class AvailableBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LibraryManagementUser> _userManager;

        public AvailableBooksController(ApplicationDbContext context, UserManager<LibraryManagementUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> ListOfAvailableBooksPage()
        {
            var availableBooks = await _context.Books
                .Where(b => !b.isBorrowed).ToListAsync();

            return View(availableBooks);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooks(string? query)
        {
            var bookSearchedCollection = await _context.Books
                .Where(b => (b.Title == query || b.Genre == query || b.ISBN == query || b.Author == query) && !b.isBorrowed )
                .ToListAsync();

            return View("ListOfAvailableBooksPage", bookSearchedCollection);
        }

        [HttpGet]
        public async Task<IActionResult> Borrow(string? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var bookToBorrow = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == id);

            if (bookToBorrow == null)
            {
                return NotFound();
            }
            return View(bookToBorrow);
        }

        [HttpPost,ActionName("Borrow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrowConfirmed(string? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var bookToBorrow = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == id);

            if (bookToBorrow != null) 
            {
                bookToBorrow.isBorrowed = true;
                _context.Update(bookToBorrow);

                var currentUser = await _userManager.GetUserAsync(User);

                var Transaction = new TransactionModel()
                {
                    UserID = currentUser.Id,
                    BorrowerName = $"{currentUser.FirstName} {currentUser.LastName}",
                    TransactionType = "Borrow",
                    BookISBN = bookToBorrow.ISBN,
                    Status = "Borrowed"
                };

                _context.Transactions.Add(Transaction);



                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListOfAvailableBooksPage));
            }

            return View(bookToBorrow);
        }
    }
}

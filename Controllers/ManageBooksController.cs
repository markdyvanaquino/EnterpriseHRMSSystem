using LibraryManagement.Database;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageBooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ManageBooksPage()
        {
            var bookList = await _context.Books.ToListAsync();

            return View(bookList);
        }

        //CREATE : Get
        public IActionResult CreateBook()
        {

            return View();
        }

        //CREATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook([Bind("Title,Description,Author,Genre,ISBN,isBorrowed")] BookModel book) 
        {
            if (ModelState.IsValid) 
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageBooksPage));
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooks(string? query)
        {
            var bookSearchedCollection = await _context.Books
                .Where(b => b.Title == query || b.Genre == query || b.ISBN == query || b.Author == query)
                .ToListAsync();

            return View("ManageBooksPage",bookSearchedCollection);
        }

        //DETAILS : Get
        [HttpGet]
        public async Task<IActionResult> BookDetails(string id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null) 
            {
                return NotFound();
            }

            return View(book);
        }

        //EDIT : Get
        [HttpGet]
        public async Task<IActionResult> EditBook(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //EDIT : POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(string id, [Bind("Title,Description,Author,Genre,ISBN,isBorrowed")] BookModel book)
        {
            if (id != book.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ISBN))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageBooksPage));
            }
            return View(book);
        }

        //GET: DELETE
        [HttpGet]
        public async Task<IActionResult> DeleteBook(string? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == id);

            if (book == null) 
            {
                return NotFound();
            }
            return View(book);
        }

        //POST: DELETE
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBookConfirmed(string? id) 
        {
            if (id == null) 
            {
                return NotFound("ID is null");
            }
            var book = await _context.Books.FindAsync(id);

            if (book != null) 
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageBooksPage));
        }

        private bool BookExists(string id) 
        {
            return _context.Books.Any(b => b.ISBN == id);
        }
    }
}

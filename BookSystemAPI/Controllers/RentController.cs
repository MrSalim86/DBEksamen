using BookSystemAPI.Models;
using BookSystemAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BookSystemAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentController : Controller
    {
        private readonly MongoService _mongo;
        private readonly MySqlDbContext _context;

        public RentController(MongoService mongo, MySqlDbContext context)
        {
            _mongo = mongo;
            _context = context;
        }

  
        [HttpGet]
        public async Task<IActionResult> GetAllBookLogs()
        {
            var logs = await _mongo.BookLogs.Find(_ => true).ToListAsync();
            return Ok(logs);
        }


        // Rent request

        [HttpPost("Abook")]
        public async Task<IActionResult> RentBook([FromBody] RentRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(); // starts a database transaction —
                                                                                     // group the following database operations together.
                                                                                     // If one of them fails, roll everything back
                                                                                     // if succuess commit all

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == request.BookId);
            if (book == null) return NotFound("Book not found.");
            // Outcomment this part to see the vesion controll part "OCC" 
            //if (book.BooksCount == 0) return BadRequest("Book is not available.");

            int originalVersion = book.Version;

            book.BooksCount = 0;
            book.Version++;

            _context.Entry(book).Property("Version").OriginalValue = originalVersion;

            try
            {
                await _context.SaveChangesAsync(); // save database
                await transaction.CommitAsync(); // commit now
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Book already rented by someone else.");
            }

            await _mongo.BookLogs.InsertOneAsync(new BookLog
            {
                BookId = request.BookId,
                UserId = request.UserId,
                Status = "rented",
                RentedAt = DateTime.UtcNow
            });

            return Ok("Book rented successfully.");
        }


        // Return Book

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnRequest request)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == request.BookId);
            if (book == null) return NotFound("Book not found.");

            book.BooksCount = 1;
            book.Version++;

            _context.Ratings.Add(new Rating
            {
                BookId = request.BookId,
                UserId = request.UserId,
                Value = request.Rating
            });

            await _context.SaveChangesAsync();

            // Recalculate average
            book.AverageRating = await _context.Ratings
                .Where(r => r.BookId == request.BookId)
                .AverageAsync(r => r.Value);
            await _context.SaveChangesAsync();

            await _mongo.Reviews.InsertOneAsync(new Review
            {
                BookId = request.BookId,
                UserId = request.UserId,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            });

            await _mongo.BookLogs.InsertOneAsync(new BookLog
            {
                BookId = request.BookId,
                UserId = request.UserId,
                Status = "returned",
                ReturnedAt = DateTime.UtcNow
            });

            return Ok("Book returned with review and rating.");
        }


    }
}

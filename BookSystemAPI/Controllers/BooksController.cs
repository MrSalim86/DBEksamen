using BookSystemAPI.Data;
using BookSystemAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BookSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly MySqlDbContext _context;
        private readonly MongoService _mongo;

        public BooksController(MySqlDbContext context, MongoService mongo)
        {
            _context = context;
            _mongo = mongo;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks(int page = 1, int pageSize = 20)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be positive numbers.");

            var totalBooks = await _context.Books.CountAsync();

            var books = await _context.Books
                .AsNoTracking()
                .OrderBy(b => b.BookId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new {
                    b.BookId,
                    b.OriginalPublicationYear,
                    b.BooksCount,
                    b.Title,
                    b.Authors,
                    b.AverageRating,
                    b.LanguageCode
                })
                .ToListAsync();

            return Ok(new
            {
                total = totalBooks,
                page,
                pageSize,
                books
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books
       .AsNoTracking()
       .Where(b => b.BookId == id)
       .Select(b => new
       {
           b.BookId,
           b.Title,
           b.Authors,
           b.Isbn,
           b.LanguageCode,
           b.AverageRating,
           b.RatingsCount,
           b.OriginalPublicationYear,
           b.BooksCount,
           b.WorkTextReviewsCount
       })
       .FirstOrDefaultAsync();

            if (book == null)
                return NotFound(new { message = $"Book with ID {id} not found." });

            // MongoDB: Get all reviews for this book
            var reviewsRaw = await _mongo.Reviews
                .Find(r => r.BookId == id)
                .SortByDescending(r => r.CreatedAt)
                .ToListAsync();

            // MySQL: Get all ratings for this book
            var ratings = await _context.Ratings
                .AsNoTracking()
                .Where(r => r.BookId == id)
                .ToListAsync();

            // Combine them by UserId
            var combined = reviewsRaw.Select(review =>
            {
                var rating = ratings.FirstOrDefault(r => r.UserId == review.UserId);
                return new ReviewWithRatingDto
                {
                    UserId = review.UserId,
                    Comment = review.Comment,
                    RatingValue = rating?.Value ?? 0,
                    CreatedAt = review.CreatedAt
                };
            }).ToList();

            return Ok(new
            {
                Book = book,
                Reviews = combined
            });
        }



        [HttpPost("search")]
        public async Task<IActionResult> SearchBooks([FromBody] BookSearch filter)
        {
            var query = _context.Books.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                string lowerTitle = filter.Title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(lowerTitle));
            }

            if (filter.MinRating.HasValue)
            {
                query = query.Where(b => b.AverageRating >= filter.MinRating.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.LanguageCode))
            {
                query = query.Where(b => b.LanguageCode == filter.LanguageCode);
            }

            if (filter.OriginalPublicationYear.HasValue)
            {
                query = query.Where(b => b.OriginalPublicationYear == filter.OriginalPublicationYear.Value);
            }

            int total = await query.CountAsync();

            var books = await query
                .OrderByDescending(b => b.AverageRating)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Authors,
                    b.AverageRating,
                    b.BooksCount,
                    b.LanguageCode,
                    b.OriginalPublicationYear
                })
                .ToListAsync();

            return Ok(new
            {
                books,
                total,
                pageSize = filter.PageSize,
                pageNumber = filter.PageNumber
            });
        }


    }
}

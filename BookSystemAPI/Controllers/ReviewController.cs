using BookSystemAPI.Models;
using BookSystemAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly MongoService _mongo;

        public ReviewController(MongoService mongo) => _mongo = mongo;

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            await _mongo.Reviews.InsertOneAsync(review);
            return Ok(new { message = "Review added" });
        }
    }
}

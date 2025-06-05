namespace BookSystemAPI.DTOs
{
    public class ReviewWithRatingDto
    {
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int RatingValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

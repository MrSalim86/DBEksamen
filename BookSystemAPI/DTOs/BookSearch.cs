namespace BookSystemAPI.DTOs
{
    public class BookSearch
    {

        public string? Title { get; set; }
        public float? MinRating { get; set; }
        public string? LanguageCode { get; set; }
        public int? OriginalPublicationYear { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

    }

}

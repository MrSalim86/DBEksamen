using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookSystemAPI.Models
{
  
        [Table("books")]
        public class Book
        {
            [Key]
            [Column("book_id")]
            public int BookId { get; set; }

            [Column("title")]
            public string Title { get; set; }

            [Column("authors")]
            public string Authors { get; set; }

            [Column("isbn")]
            public string Isbn { get; set; }

            [Column("language_code")]
            public string LanguageCode { get; set; }

            [Column("average_rating")]
            public double AverageRating { get; set; }

            [Column("ratings_count")]
            public int RatingsCount { get; set; }

            [Column("original_publication_year")]
            public int OriginalPublicationYear { get; set; }

            [Column("books_count")]
            public int BooksCount { get; set; }

            [Column("work_text_reviews_count")]
            public int WorkTextReviewsCount { get; set; }

            [Column("version")]
            public int Version { get; set; }

            public ICollection<Rating> Ratings { get; set; }
        }
   
}

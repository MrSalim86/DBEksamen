using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookSystemAPI.Models
{
    [Table("ratings")]
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        [Column("book_id")]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        public User User { get; set; }

        [Column("rating")]
        public int Value { get; set; }
    }
}

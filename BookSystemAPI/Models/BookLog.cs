using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookSystemAPI.Models
{
    public class BookLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("book_id")]
        public int BookId { get; set; }

        [BsonElement("rented_at")]
        public DateTime RentedAt { get; set; }

        [BsonElement("returned_at")]
        public DateTime? ReturnedAt { get; set; }
        
        [BsonElement("status")]
        public string Status { get; set; }
    }
}

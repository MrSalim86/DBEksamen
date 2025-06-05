using BookSystemAPI.Models;
using MongoDB.Driver;

namespace BookSystemAPI.Data
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase(configuration["MongoDatabase"]);
        }

        public IMongoCollection<Review> Reviews => _database.GetCollection<Review>("reviews");
        public IMongoCollection<BookLog> BookLogs => _database.GetCollection<BookLog>("booklogs");
    }
}

using MongoDB.Driver;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Services.Implementations
{
    public class MongoLogService : IMongoLogServices
    {
        private readonly IMongoCollection<LogModel> _logs;
        public MongoLogService()
        {
            // 👇 change connection string if needed
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ShopLogs");
            _logs = database.GetCollection<LogModel>("Logs");
        }

        public void InsertLog(string eventType, string? description, string? userId)
        {
            var log = new LogModel
            {
                EventType = eventType,
                Description = description,
                UserID = userId
            };

            _logs.InsertOne(log);
        }
    }
}
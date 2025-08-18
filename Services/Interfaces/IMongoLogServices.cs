namespace MyWebApiApp.Services.Interfaces
{
    public interface IMongoLogServices
    {
        void InsertLog(string eventType, string? description, string? userId);
    }
}
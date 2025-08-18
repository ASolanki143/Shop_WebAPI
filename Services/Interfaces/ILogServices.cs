namespace MyWebApiApp.Services.Interfaces
{
    public interface ILogServices
    {
        void InsertLog(string eventType, string? description, string? userId);
    }
}
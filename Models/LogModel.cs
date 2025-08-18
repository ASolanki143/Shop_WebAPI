using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class LogModel
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string EventType { get; set; }   // e.g. Login, Logout, ProductUpdated
    public string? UserID { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

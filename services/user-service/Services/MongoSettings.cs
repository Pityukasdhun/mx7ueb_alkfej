namespace user_service.Services;

public class MongoSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string UsersCollection { get; set; } = string.Empty;
}
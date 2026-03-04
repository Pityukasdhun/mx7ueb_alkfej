using Microsoft.Extensions.Options;
using MongoDB.Driver;
using user_service.Models;

namespace user_service.Services;

public class UsersService
{
    private readonly IMongoCollection<UserProfile> _users;

    public UsersService(IOptions<MongoSettings> settings)
    {
        var cfg = settings.Value;
        var client = new MongoClient(cfg.ConnectionString);
        var db = client.GetDatabase(cfg.Database);
        _users = db.GetCollection<UserProfile>(cfg.UsersCollection);
    }

    public async Task<List<UserProfile>> GetAllAsync() =>
        await _users.Find(_ => true).ToListAsync();

    public async Task<UserProfile> CreateAsync(UserProfile user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }
}
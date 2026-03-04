using book_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace book_service.Services;

public class BooksService
{
    private readonly IMongoCollection<Book> _books;

    public BooksService(IOptions<MongoSettings> settings)
    {
        var cfg = settings.Value;

        var client = new MongoClient(cfg.ConnectionString);
        var db = client.GetDatabase(cfg.Database);
        _books = db.GetCollection<Book>(cfg.BooksCollection);
    }

    public async Task<List<Book>> GetAllAsync() =>
        await _books.Find(_ => true).ToListAsync();

    public async Task<Book?> GetByIdAsync(string id) =>
        await _books.Find(b => b.Id == id).FirstOrDefaultAsync();

    public async Task<List<Book>> SearchAsync(string? q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return await GetAllAsync();

        q = q.Trim();

        var filter = Builders<Book>.Filter.Or(
            Builders<Book>.Filter.Regex(b => b.Title, new MongoDB.Bson.BsonRegularExpression(q, "i")),
            Builders<Book>.Filter.Regex(b => b.Author, new MongoDB.Bson.BsonRegularExpression(q, "i"))
        );

        return await _books.Find(filter).ToListAsync();
    }

    public async Task<Book> CreateAsync(Book book)
    {
        await _books.InsertOneAsync(book);
        return book;
    }

    public async Task<bool> UpdateAsync(string id, Book updated)
    {
        updated.Id = id;
        var result = await _books.ReplaceOneAsync(b => b.Id == id, updated);
        return result.MatchedCount == 1;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _books.DeleteOneAsync(b => b.Id == id);
        return result.DeletedCount == 1;
    }
}
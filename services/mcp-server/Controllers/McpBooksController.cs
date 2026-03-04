using Microsoft.AspNetCore.Mvc;
using mcp_server.Clients;
using mcp_server.Dtos;

namespace mcp_server.Controllers;

[ApiController]
[Route("api/mcp/books")]
public class McpBooksController : ControllerBase
{
    private readonly BooksClient _books;

    public McpBooksController(BooksClient books)
    {
        _books = books;
    }

    // GET /api/mcp/books?q=asimov
    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> List([FromQuery] string? q)
    {
        var result = await _books.ListAsync(q);
        return Ok(result);
    }

    // POST /api/mcp/books
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Title) || string.IsNullOrWhiteSpace(req.Author))
            return BadRequest("Title and Author are required.");

        var created = await _books.CreateAsync(req);
        if (created is null) return StatusCode(502, "Downstream book-service error.");

        return Ok(created);
    }
}
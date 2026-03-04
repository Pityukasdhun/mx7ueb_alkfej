using book_service.Dtos;
using book_service.Models;
using book_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace book_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _service;

    public BooksController(BooksService service)
    {
        _service = service;
    }

    // GET /api/books?q=asimov
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAll([FromQuery] string? q)
    {
        var books = await _service.SearchAsync(q);
        return Ok(books);
    }

    // GET /api/books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById(string id)
    {
        var book = await _service.GetByIdAsync(id);
        if (book is null) return NotFound();
        return Ok(book);
    }

    // POST /api/books
    [HttpPost]
    public async Task<ActionResult<Book>> Create([FromBody] CreateBookRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Title) || string.IsNullOrWhiteSpace(req.Author))
            return BadRequest("Title and Author are required.");

        var book = new Book
        {
            Title = req.Title.Trim(),
            Author = req.Author.Trim(),
            Year = req.Year
        };

        var created = await _service.CreateAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateBookRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Title) || string.IsNullOrWhiteSpace(req.Author))
            return BadRequest("Title and Author are required.");

        var ok = await _service.UpdateAsync(id, new Book
        {
            Title = req.Title.Trim(),
            Author = req.Author.Trim(),
            Year = req.Year
        });

        if (!ok) return NotFound();
        return NoContent();
    }

    // DELETE /api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
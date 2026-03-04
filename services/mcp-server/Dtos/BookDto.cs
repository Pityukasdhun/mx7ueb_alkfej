namespace mcp_server.Dtos;

public class BookDto
{
    public string? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
}
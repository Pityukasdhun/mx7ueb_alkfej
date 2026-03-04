using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using mcp_server.Dtos;

namespace mcp_server.Clients;

public class DownstreamOptions
{
    public string BooksBaseUrl { get; set; } = string.Empty;
}

public class BooksClient
{
    private readonly HttpClient _http;
    private readonly DownstreamOptions _opt;

    public BooksClient(HttpClient http, IOptions<DownstreamOptions> opt)
    {
        _http = http;
        _opt = opt.Value;
    }

    public async Task<List<BookDto>> ListAsync(string? q)
    {
        var url = $"{_opt.BooksBaseUrl}/api/Books";
        if (!string.IsNullOrWhiteSpace(q))
            url += $"?q={Uri.EscapeDataString(q.Trim())}";

        var data = await _http.GetFromJsonAsync<List<BookDto>>(url);
        return data ?? new List<BookDto>();
    }

    public async Task<BookDto?> CreateAsync(CreateBookRequest req)
    {
        var url = $"{_opt.BooksBaseUrl}/api/Books";
        var resp = await _http.PostAsJsonAsync(url, req);

        if (!resp.IsSuccessStatusCode)
            return null;

        return await resp.Content.ReadFromJsonAsync<BookDto>();
    }
}
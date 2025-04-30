namespace GoodBurguerAPI.DTOs.Response;

public record ErrorDTO
{
    public string? Message { get; init; }
    public string? Detail { get; init; }
};
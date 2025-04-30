using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.DTOs.Request;

public record CreateOrderDTO
{
    public int? SandwichId { get; set; }
    public int? ExtraId { get; set; }
    
    public int? DrinkId { get; set; }
}
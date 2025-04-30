namespace GoodBurguerAPI.DTOs.Request;

public record UpdateOrderDTO : CreateOrderDTO
{
    public int OrderId { get; set; }
};
using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.DTOs.Response;

public record OrderPriceDTO
{
    public int OrderId { get; init; }
    public decimal Price { get; init; }
    
    public static explicit operator OrderPriceDTO(Order order)
    {
        var dto = new OrderPriceDTO
        {
            OrderId = order.OrderId,
            Price = order.Price,
        };
        return dto;
    }
};
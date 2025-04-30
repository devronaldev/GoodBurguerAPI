using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.DTOs.Response;

/// <summary>
/// Represents the response data containing order ID and total price.
/// </summary>
public record OrderPriceDTO
{
    /// <summary>
    /// The unique identifier of the order.
    /// </summary>
    public int OrderId { get; init; }

    /// <summary>
    /// The final calculated price of the order.
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Explicitly converts an <see cref="Order"/> entity to an <see cref="OrderPriceDTO"/>.
    /// </summary>
    /// <param name="order">The order entity to convert.</param>
    /// <returns>An <see cref="OrderPriceDTO"/> containing order ID and price.</returns>
    public static explicit operator OrderPriceDTO(Order order)
    {
        var dto = new OrderPriceDTO
        {
            OrderId = order.OrderId,
            Price = order.Price,
        };
        return dto;
    }
}
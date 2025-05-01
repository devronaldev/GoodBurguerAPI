using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.DTOs.Response;

public record ViewOrderDTO
{
    public int OrderId { get; init; }
    public decimal Price { get; init; }
    public decimal Discount
    {
        get
        {
            var totalItemsPrice = 
                (Sandwich?.ItemPrice ?? 0) +
                (Extra?.ItemPrice ?? 0) +
                (Drink?.ItemPrice ?? 0);

            return totalItemsPrice - Price;
        }
    }
    public Item? Sandwich { get; init; }
    public Item? Extra { get; init; }
    public Item? Drink { get; init; }
}
using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.DTOs.Request;

/// <summary>
/// Represents the data required to create a new order.
/// </summary>
public record CreateOrderDTO
{
    /// <summary>
    /// The ID of the selected sandwich.
    /// </summary>
    public int? SandwichId { get; set; }

    /// <summary>
    /// The ID of the selected extra item (optional).
    /// </summary>
    public int? ExtraId { get; set; }

    /// <summary>
    /// The ID of the selected drink (optional).
    /// </summary>
    public int? DrinkId { get; set; }
}
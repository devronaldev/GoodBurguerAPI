namespace GoodBurguerAPI.DTOs.Request;

/// <summary>
/// Represents the data required to update an existing order.
/// Inherits item selection fields from <see cref="CreateOrderDTO"/>.
/// </summary>
public record UpdateOrderDTO : CreateOrderDTO
{
    /// <summary>
    /// The unique identifier of the order to be updated.
    /// </summary>
    public int OrderId { get; set; }
}
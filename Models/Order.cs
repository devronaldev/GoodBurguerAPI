namespace GoodBurguerAPI.Models;

/// <summary>
/// Represents an order with a sandwich and/or extras with its payment price.
/// </summary>
public sealed class Order
{
    /// <summary>
    /// Identifier of the order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Payment price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The Sandwich which is not obligatory in the order. (Optional)
    /// </summary>
    public Item? Sandwich { get; set; }

    /// <summary>
    /// The Extra which is not obligatory in the order. (Optional)
    /// </summary>
    public Item? Extra { get; set; }
    
    /// <summary>
    /// The Extra which is not obligatory in the order. (Optional)
    /// </summary>
    public Item? Drink { get; set; }

    /// <summary>
    /// Contructor to initialize a new order with its price and optional items. (There will be at least one)
    /// </summary>
    /// <param name="price">Payment Price.</param>
    /// <param name="sandwich">The sandwich. (optional)</param>
    /// <param name="extra">The extra item. (optional)</param>
    /// <param name="drink">The drink</param>
    public Order(decimal price = 0, Item? sandwich = null, Item? extra = null, Item? drink = null)
    {
        Price = price;
        Sandwich = sandwich;
        Extra = extra;
        Drink = drink;
    }
}
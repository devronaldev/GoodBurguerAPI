namespace GoodBurguerAPI.Models;

/// <summary>
/// Represents an order with a sandwich and/or extras with its payment price.
/// </summary>
public sealed class Order
{
    private Item? _sandwich;
    private Item? _extra;
    private Item? _drink;
    
    /// <summary>
    /// Identifier of the order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Payment price.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// The identifier of the sandwich included in the order, if any.
    /// </summary>
    public int? SandwichId { get; private set; }

    /// <summary>
    /// The identifier of the extra item included in the order, if any.
    /// </summary>
    public int? ExtraId { get; private set; }

    /// <summary>
    /// The identifier of the drink included in the order, if any.
    /// </summary>
    public int? DrinkId { get; private set; }

    /// <summary>
    /// The Sandwich which is not obligatory in the order. (Optional)
    /// </summary>
    public Item? Sandwich
    {
        get => _sandwich;
        set
        {
            _sandwich = value;
            SandwichId = value?.ItemId;
        }
    }

    /// <summary>
    /// The Extra which is not obligatory in the order. (Optional)
    /// </summary>
    public Item? Extra 
    { 
        get => _extra;
        set
        {
            _extra = value;
            ExtraId = value?.ItemId;
        }
    }

    /// <summary>
    /// The Extra which is not obligatory in the order. (Optional)
    /// </summary>
    public Item? Drink
    {
        get => _drink;
        set
        {
            _drink = value;
            DrinkId = value?.ItemId;
        }
    }

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

    public Order()
    {
        
    }
}
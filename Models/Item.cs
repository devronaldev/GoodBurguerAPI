using GoodBurguerAPI.Models.Enum;

namespace GoodBurguerAPI.Models;

/// <summary>
/// Represents an item that can be part of an order, such as a sandwich or an extra.
/// </summary>
public class Item
{
    /// <summary>
    /// Identifier of the item.
    /// </summary>
    public int ItemId { get; set; }

    /// <summary>
    /// Name of the item.
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// Price of the item.
    /// </summary>
    public decimal ItemPrice { get; set; }

    /// <summary>
    /// Type of the item (e.g., Sandwich, Extra).
    /// </summary>
    public EItemType ItemType { get; set; }
}
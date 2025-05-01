using GoodBurguerAPI.Models;
using GoodBurguerAPI.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodBurguerAPI.Configurations.ModelsConfiguration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.ItemId);
        
        builder.Property(x => x.ItemPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasData(
            new Item {ItemId = 1, ItemName = "X Burguer", ItemPrice = 5.00m, ItemType = EItemType.Sandwich },
            new Item {ItemId = 2, ItemName = "X Egg", ItemPrice = 4.50m, ItemType = EItemType.Sandwich},
            new Item {ItemId = 3, ItemName = "X Bacon", ItemPrice = 7.00m, ItemType = EItemType.Sandwich},
            new Item {ItemId = 4, ItemName = "Fries", ItemPrice = 2.00m, ItemType = EItemType.Extra},
            new Item {ItemId = 5, ItemName = "Soft drink", ItemPrice = 2.50m, ItemType = EItemType.Drink}
        );
    }
}
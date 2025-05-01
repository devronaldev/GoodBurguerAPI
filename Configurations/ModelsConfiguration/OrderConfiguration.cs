using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoodBurguerAPI.Models;

namespace GoodBurguerAPI.Configurations
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Order"/> entity.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Configures the database schema for the <see cref="Order"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary key
            builder.HasKey(x => x.OrderId);

            // Relationship: Order → Sandwich
            builder.HasOne(x => x.Sandwich)
                .WithMany()
                .HasForeignKey(x => x.SandwichId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Order → Extra
            builder.HasOne(x => x.Extra)
                .WithMany()
                .HasForeignKey(x => x.ExtraId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Order → Drink
            builder.HasOne(x => x.Drink)
                .WithMany()
                .HasForeignKey(x => x.DrinkId)
                .OnDelete(DeleteBehavior.Restrict);

            // Property configuration: Price
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
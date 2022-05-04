using Customers.Model.Entities;
using Customers.Model.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Data.Configurations
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .ToTable("Customers");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Email)
                .IsRequired()
                .HasConversion(
                    e => e.Value,
                    s => new Email(s))
                .HasMaxLength(200);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}

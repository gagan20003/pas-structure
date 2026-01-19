// File: InsuranceSystem.Modules.Product.Infrastructure/Configurations/ProductConfiguration.cs
using InsuranceSystem.Modules.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Product.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for Product entity.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
    {
        builder.ToTable("PRODUCTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Properties
        builder.Property(e => e.ProductCode)
            .HasColumnName("PRODUCT_CODE")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(2000);

        // Enum to int conversions
        builder.Property(e => e.ProductType)
            .HasColumnName("PRODUCT_TYPE")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.CurrencyId)
            .HasColumnName("CURRENCY_ID")
            .IsRequired();

        // Decimal precision for monetary values
        builder.Property(e => e.BasePremium)
            .HasColumnName("BASE_PREMIUM")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(e => e.MaxCoverageAmount)
            .HasColumnName("MAX_COVERAGE_AMOUNT")
            .HasPrecision(18, 2);

        builder.Property(e => e.DeductibleAmount)
            .HasColumnName("DEDUCTIBLE_AMOUNT")
            .HasPrecision(18, 2);

        // DateOnly for dates without time
        builder.Property(e => e.EffectiveDate)
            .HasColumnName("EFFECTIVE_DATE")
            .IsRequired();

        builder.Property(e => e.ExpirationDate)
            .HasColumnName("EXPIRATION_DATE");

        // Audit fields
        builder.Property(e => e.CreatedAt)
            .HasColumnName("CREATED_ON")
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasColumnName("CREATED_BY")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("MODIFIED_ON");

        builder.Property(e => e.UpdatedBy)
            .HasColumnName("MODIFIED_BY")
            .HasMaxLength(100);

        // Indexes
        builder.HasIndex(e => e.ProductCode)
            .IsUnique()
            .HasDatabaseName("IX_PRODUCTS_PRODUCT_CODE");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_PRODUCTS_STATUS");

        builder.HasIndex(e => e.ProductType)
            .HasDatabaseName("IX_PRODUCTS_PRODUCT_TYPE");
    }
}

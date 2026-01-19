// File: InsuranceSystem.Modules.Contract.Infrastructure/Configurations/ContractConfiguration.cs
using InsuranceSystem.Modules.Contract.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Contract.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for Contract entity.
/// </summary>
public class ContractConfiguration : IEntityTypeConfiguration<Domain.Entities.Contract>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Contract> builder)
    {
        builder.ToTable("CONTRACTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Properties
        builder.Property(e => e.ContractNumber)
            .HasColumnName("CONTRACT_NUMBER")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.MasterContractId)
            .HasColumnName("MASTER_CONTRACT_ID")
            .IsRequired();

        builder.Property(e => e.ProductId)
            .HasColumnName("PRODUCT_ID")
            .IsRequired();

        // Enum conversion
        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.CurrencyId)
            .HasColumnName("CURRENCY_ID")
            .IsRequired();

        // Decimal precision for monetary values
        builder.Property(e => e.PremiumAmount)
            .HasColumnName("PREMIUM_AMOUNT")
            .HasPrecision(18, 2)
            .IsRequired();

        // DateOnly for dates
        builder.Property(e => e.EffectiveDate)
            .HasColumnName("EFFECTIVE_DATE")
            .IsRequired();

        builder.Property(e => e.ExpirationDate)
            .HasColumnName("EXPIRATION_DATE")
            .IsRequired();

        builder.Property(e => e.TerminationDate)
            .HasColumnName("TERMINATION_DATE");

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

        // Relationships
        builder.HasOne(e => e.MasterContract)
            .WithMany(mc => mc.Contracts)
            .HasForeignKey(e => e.MasterContractId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_CONTRACTS_MASTER_CONTRACT");

        // Indexes
        builder.HasIndex(e => e.ContractNumber)
            .IsUnique()
            .HasDatabaseName("IX_CONTRACTS_CONTRACT_NUMBER");

        builder.HasIndex(e => e.MasterContractId)
            .HasDatabaseName("IX_CONTRACTS_MASTER_CONTRACT_ID");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_CONTRACTS_STATUS");
    }
}

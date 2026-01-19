// File: InsuranceSystem.Modules.Contract.Infrastructure/Configurations/MasterContractConfiguration.cs
using InsuranceSystem.Modules.Contract.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Contract.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for MasterContract entity.
/// </summary>
public class MasterContractConfiguration : IEntityTypeConfiguration<MasterContract>
{
    public void Configure(EntityTypeBuilder<MasterContract> builder)
    {
        builder.ToTable("MASTER_CONTRACTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Properties
        builder.Property(e => e.MasterContractNumber)
            .HasColumnName("MASTER_CONTRACT_NUMBER")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.PolicyholderName)
            .HasColumnName("POLICYHOLDER_NAME")
            .HasMaxLength(200)
            .IsRequired();

        // Enum conversion
        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.CurrencyId)
            .HasColumnName("CURRENCY_ID")
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

        // Indexes
        builder.HasIndex(e => e.MasterContractNumber)
            .IsUnique()
            .HasDatabaseName("IX_MASTER_CONTRACTS_NUMBER");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_MASTER_CONTRACTS_STATUS");
    }
}

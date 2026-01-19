// File: InsuranceSystem.Modules.Contract.Infrastructure/Configurations/EndorsementConfiguration.cs
using InsuranceSystem.Modules.Contract.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Contract.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for Endorsement entity.
/// </summary>
public class EndorsementConfiguration : IEntityTypeConfiguration<Endorsement>
{
    public void Configure(EntityTypeBuilder<Endorsement> builder)
    {
        builder.ToTable("ENDORSEMENTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Properties
        builder.Property(e => e.EndorsementNumber)
            .HasColumnName("ENDORSEMENT_NUMBER")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.ContractId)
            .HasColumnName("CONTRACT_ID")
            .IsRequired();

        // Enum conversions
        builder.Property(e => e.EndorsementType)
            .HasColumnName("ENDORSEMENT_TYPE")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        // DateOnly for dates
        builder.Property(e => e.EffectiveDate)
            .HasColumnName("EFFECTIVE_DATE")
            .IsRequired();

        // Decimal precision for monetary values
        builder.Property(e => e.PremiumAdjustment)
            .HasColumnName("PREMIUM_ADJUSTMENT")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(1000);

        builder.Property(e => e.ProcessedOn)
            .HasColumnName("PROCESSED_ON");

        builder.Property(e => e.ProcessedBy)
            .HasColumnName("PROCESSED_BY")
            .HasMaxLength(100);

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
        builder.HasOne(e => e.Contract)
            .WithMany(c => c.Endorsements)
            .HasForeignKey(e => e.ContractId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ENDORSEMENTS_CONTRACT");

        // Indexes
        builder.HasIndex(e => e.EndorsementNumber)
            .IsUnique()
            .HasDatabaseName("IX_ENDORSEMENTS_NUMBER");

        builder.HasIndex(e => e.ContractId)
            .HasDatabaseName("IX_ENDORSEMENTS_CONTRACT_ID");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_ENDORSEMENTS_STATUS");
    }
}

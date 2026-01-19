// File: InsuranceSystem.Modules.Member.Infrastructure/Configurations/MemberConfiguration.cs
using InsuranceSystem.Modules.Member.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Member.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for Member entity.
/// </summary>
public class MemberConfiguration : IEntityTypeConfiguration<Domain.Entities.Member>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Member> builder)
    {
        builder.ToTable("MEMBERS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Properties
        builder.Property(e => e.MemberNumber)
            .HasColumnName("MEMBER_NUMBER")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasColumnName("FIRST_NAME")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasColumnName("LAST_NAME")
            .HasMaxLength(100)
            .IsRequired();

        // DateOnly for date without time
        builder.Property(e => e.DateOfBirth)
            .HasColumnName("DATE_OF_BIRTH")
            .IsRequired();

        // Enum to int conversion
        builder.Property(e => e.Gender)
            .HasColumnName("GENDER")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(255);

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("PHONE_NUMBER")
            .HasMaxLength(20);

        builder.Property(e => e.Address)
            .HasColumnName("ADDRESS")
            .HasMaxLength(500);

        builder.Property(e => e.ContractId)
            .HasColumnName("CONTRACT_ID")
            .IsRequired();

        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.EffectiveDate)
            .HasColumnName("EFFECTIVE_DATE")
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

        // Ignore computed property
        builder.Ignore(e => e.FullName);

        // Indexes
        builder.HasIndex(e => e.MemberNumber)
            .IsUnique()
            .HasDatabaseName("IX_MEMBERS_MEMBER_NUMBER");

        builder.HasIndex(e => e.ContractId)
            .HasDatabaseName("IX_MEMBERS_CONTRACT_ID");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_MEMBERS_STATUS");

        builder.HasIndex(e => new { e.LastName, e.FirstName })
            .HasDatabaseName("IX_MEMBERS_NAME");
    }
}

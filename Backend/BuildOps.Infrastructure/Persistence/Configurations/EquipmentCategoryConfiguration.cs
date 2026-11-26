using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildOps.Infrastructure.Persistence.Configurations;

public class EquipmentCategoryConfiguration
    : IEntityTypeConfiguration<EquipmentCategory>
{
    public void Configure(
        EntityTypeBuilder<EquipmentCategory> builder)
    {
        builder.ToTable("EquipmentCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .UseCollation("Latin1_General_100_CI_AS");

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}
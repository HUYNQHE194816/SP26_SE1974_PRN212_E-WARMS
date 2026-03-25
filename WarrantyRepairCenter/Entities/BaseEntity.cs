using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarrantyRepairCenter.ValueGenerators;

namespace WarrantyRepairCenter.Entities;

public abstract class BaseEntity
{
    public Guid ID { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime LastModifiedTime { get; set; }
    public bool Deleted { get; set; }
}

public abstract class EntityCfg<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(c => c.ID);
        builder.Property(c => c.ID).HasColumnName("id").HasValueGenerator<UUIDGenerator>();
        builder.Property(c => c.CreationTime).HasColumnName("creation_time").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(c => c.LastModifiedTime).HasColumnName("last_modified_time").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(c => c.Deleted).HasColumnName("deleted").HasDefaultValue(false);
        builder.HasIndex(c => c.ID).IsUnique();
    }
}
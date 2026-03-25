using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace SP26_SE1974_PRN212_E_WARMS;

public partial class WarrantyRepairCenterContext : DbContext
{
    public WarrantyRepairCenterContext()
    {
    }

    public WarrantyRepairCenterContext(DbContextOptions<WarrantyRepairCenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<RepairOrder> RepairOrders { get; set; }

    public virtual DbSet<RepairOrderPart> RepairOrderParts { get; set; }

    public virtual DbSet<SparePart> SpareParts { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionString"];

        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D88CB8C092");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Devices__49E12311D3214A01");

            entity.Property(e => e.DeviceName).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.Devices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devices__Custome__3A81B327");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAB5CD710146");

            entity.Property(e => e.IssuedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.RepairOrder).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.RepairOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__Repair__4F7CD00D");
        });

        modelBuilder.Entity<RepairOrder>(entity =>
        {
            entity.HasKey(e => e.RepairOrderId).HasName("PK__RepairOr__016C098E52A081FC");

            entity.Property(e => e.CompletedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LaborCost)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Device).WithMany(p => p.RepairOrders)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RepairOrd__Devic__4316F928");

            entity.HasOne(d => d.Technician).WithMany(p => p.RepairOrders)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("FK__RepairOrd__Techn__440B1D61");
        });

        modelBuilder.Entity<RepairOrderPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RepairOr__3214EC0773902BE2");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Part).WithMany(p => p.RepairOrderParts)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RepairOrd__PartI__4BAC3F29");

            entity.HasOne(d => d.RepairOrder).WithMany(p => p.RepairOrderParts)
                .HasForeignKey(d => d.RepairOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RepairOrd__Repai__4AB81AF0");
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__SparePar__7C3F0D503185C617");

            entity.Property(e => e.PartName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Stock).HasDefaultValue(0);
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.TechnicianId).HasName("PK__Technici__301F8121BFDFFD02");

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.SkillLevel).HasMaxLength(50);
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__Warranty__2ED3181378ACA34A");

            entity.ToTable("Warranty");

            entity.Property(e => e.WarrantyMonths).HasDefaultValue(3);

            entity.HasOne(d => d.Device).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Warranty__Device__534D60F1");

            entity.HasOne(d => d.RepairOrder).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.RepairOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Warranty__Repair__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

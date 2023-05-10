using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAn_be.Entities;

public partial class DatabaseDoanContext : DbContext
{
    public DatabaseDoanContext()
    {
    }

    public DatabaseDoanContext(DbContextOptions<DatabaseDoanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<DetailOrder> DetailOrders { get; set; }

    public virtual DbSet<ImageProduct> ImageProducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAS\\WINCCPLUSMIG2014;Initial Catalog=Database_doan;Integrated Security=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.IdAdmins).HasName("PK__Admins__6D90184AC2EF2D3E");

            entity.Property(e => e.IdAdmins).HasColumnName("ID_Admins");
            entity.Property(e => e.AccountAdmins)
                .HasMaxLength(255)
                .HasColumnName("Account_Admins");
            entity.Property(e => e.PasswordAdmins)
                .HasMaxLength(255)
                .HasColumnName("Password_Admins");
        });

        modelBuilder.Entity<DetailOrder>(entity =>
        {
            entity.HasKey(e => e.IdDetailOrders).HasName("PK__DetailOr__15D238810C5EC0A8");

            entity.Property(e => e.IdDetailOrders).HasColumnName("ID_DetailOrders");
            entity.Property(e => e.IdOrders).HasColumnName("ID_Orders");
            entity.Property(e => e.PriceDetailOrders)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Price_DetailOrders");
            entity.Property(e => e.QuantityDetailOrders).HasColumnName("Quantity_DetailOrders");

            entity.HasOne(d => d.IdOrdersNavigation).WithMany(p => p.DetailOrders)
                .HasForeignKey(d => d.IdOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetailOrd__ID_Or__1BFD2C07");
        });

        modelBuilder.Entity<ImageProduct>(entity =>
        {
            entity.HasKey(e => e.IdImageProduct).HasName("PK__ImagePro__E0FE4D2B5A70DCF4");

            entity.ToTable("ImageProduct");

            entity.Property(e => e.IdImageProduct).HasColumnName("ID_ImageProduct");
            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.ImageNameProduct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageName_Product");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ImageProducts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImageProd__ID_Pr__1CF15040");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrders).HasName("PK__Orders__20F81C1DAA4F9645");

            entity.Property(e => e.IdOrders).HasColumnName("ID_Orders");
            entity.Property(e => e.DayCreate)
                .HasColumnType("datetime")
                .HasColumnName("Day_Create");
            entity.Property(e => e.IdUsers).HasColumnName("ID_Users");
            entity.Property(e => e.Statuss)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(38, 2)");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ID_Users__1B0907CE");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__522DE49616E85E09");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.DayCreate)
                .HasColumnType("datetime")
                .HasColumnName("Day_Create");
            entity.Property(e => e.DetailProduct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Detail_Product");
            entity.Property(e => e.Hsd)
                .HasColumnType("date")
                .HasColumnName("HSD");
            entity.Property(e => e.IdAdmins).HasColumnName("ID_Admins");
            entity.Property(e => e.IngredientProduct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Ingredient_Product");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name_Product");
            entity.Property(e => e.PriceProduct)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Price_Product");
            entity.Property(e => e.QuantityProduct).HasColumnName("Quantity_Product");
            entity.Property(e => e.TagProduct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Tag_Product");

            entity.HasOne(d => d.IdAdminsNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdAdmins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__ID_Admi__1A14E395");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PK__Users__B97FFDA1D121DC31");

            entity.Property(e => e.IdUsers).HasColumnName("ID_Users");
            entity.Property(e => e.AddressUsers)
                .HasMaxLength(255)
                .HasColumnName("Address_users");
            entity.Property(e => e.ImagepathUsers)
                .HasMaxLength(255)
                .HasColumnName("Imagepath_users");
            entity.Property(e => e.PasswordUsers)
                .HasMaxLength(255)
                .HasColumnName("Password_users");
            entity.Property(e => e.PhoneNumberUsers)
                .HasMaxLength(255)
                .HasColumnName("PhoneNumber_users");
            entity.Property(e => e.UsernameUsers)
                .HasMaxLength(255)
                .HasColumnName("Username_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

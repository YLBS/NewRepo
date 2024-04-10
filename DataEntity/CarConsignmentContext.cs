using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataEntity;

public partial class CarConsignmentContext : DbContext
{
    public CarConsignmentContext()
    {
    }

    public CarConsignmentContext(DbContextOptions<CarConsignmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthCode> AuthCodes { get; set; }

    public virtual DbSet<CarInfo> CarInfos { get; set; }

    public virtual DbSet<ConsignmentOrder> ConsignmentOrders { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<NewSalesTicket> NewSalesTickets { get; set; }

    public virtual DbSet<Power> Powers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<SaleAfterOrder> SaleAfterOrders { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<VehicleLevel> VehicleLevels { get; set; }

    public virtual DbSet<WorkFlow> WorkFlows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CarConsignment;User ID=sa;Password=Yzz123456;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuthCode__3213E83FB484378C");

            entity.ToTable("AuthCode");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.ExpirationTime)
                .HasColumnType("datetime")
                .HasColumnName("expirationTime");
            entity.Property(e => e.Phone).HasMaxLength(11);
        });

        modelBuilder.Entity<CarInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarInfo__3214EC07AE9E89FB");

            entity.ToTable("CarInfo");

            entity.Property(e => e.Appearance).HasMaxLength(1000);
            entity.Property(e => e.BrandName).HasMaxLength(10);
            entity.Property(e => e.DownPayment).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.Fuel).HasMaxLength(10);
            entity.Property(e => e.Mileage).HasColumnType("numeric(9, 2)");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Registration).HasMaxLength(100);
            entity.Property(e => e.SellingPrice).HasColumnType("numeric(8, 2)");
            entity.Property(e => e.TransmissionType).HasMaxLength(5);
            entity.Property(e => e.VehiTrim).HasMaxLength(1000);
            entity.Property(e => e.VehicleAge).HasColumnName("vehicleAge");
            entity.Property(e => e.VehicleColor).HasMaxLength(10);
            entity.Property(e => e.VehicleCondition).HasMaxLength(500);
            entity.Property(e => e.VehicleLevelName).HasMaxLength(20);
            entity.Property(e => e.VehicleSource).HasMaxLength(20);
            entity.Property(e => e.VehicleState).HasMaxLength(10);
        });

        modelBuilder.Entity<ConsignmentOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Consignm__3214EC07FEAB553C");

            entity.ToTable("ConsignmentOrder");

            entity.Property(e => e.Condition).HasMaxLength(100);
            entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.State).HasMaxLength(10);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC0724A7EE39");

            entity.ToTable("Menu");

            entity.Property(e => e.Click)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Icon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<NewSalesTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewSales__3214EC07CBE7BABF");

            entity.ToTable("NewSalesTicket");

            entity.Property(e => e.Idea1)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Idea2)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Reason1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Reason2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Power>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Power__3213E83FA0920FCE");

            entity.ToTable("Power");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07D76CF302");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Route__3214EC07F4E2EC77");

            entity.ToTable("Route");

            entity.Property(e => e.RouteName).HasMaxLength(20);
        });

        modelBuilder.Entity<SaleAfterOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SaleAfte__3214EC0727775DED");

            entity.ToTable("SaleAfterOrder");

            entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.State).HasMaxLength(10);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC0714A7162A");

            entity.ToTable("UserInfo");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CardNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HeadPortrait).HasMaxLength(100);
            entity.Property(e => e.IdNumber)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.Mailbox).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.OpeningBank).HasMaxLength(100);
            entity.Property(e => e.PassWord).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VehicleLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleL__3214EC07FE83663A");

            entity.ToTable("VehicleLevel");

            entity.Property(e => e.LevelName).HasMaxLength(10);
        });

        modelBuilder.Entity<WorkFlow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkFlow__3214EC071F4A1229");

            entity.ToTable("WorkFlow");

            entity.Property(e => e.WorkFlowName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

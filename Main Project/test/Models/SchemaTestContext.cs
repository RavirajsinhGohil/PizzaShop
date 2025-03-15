﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

public partial class SchemaTestContext : DbContext
{
    public SchemaTestContext()
    {
    }

    public SchemaTestContext(DbContextOptions<SchemaTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Loginuser> Loginusers { get; set; }

    public virtual DbSet<Mainnavbar> Mainnavbars { get; set; }

    public virtual DbSet<Menucategory> Menucategories { get; set; }

    public virtual DbSet<Modifiergroup> Modifiergroups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Ordermodifierdetail> Ordermodifierdetails { get; set; }

    public virtual DbSet<Orderrating> Orderratings { get; set; }

    public virtual DbSet<Orderstatus> Orderstatuses { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Tablegrouping> Tablegroupings { get; set; }

    public virtual DbSet<Taxesandfee> Taxesandfees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userfavouriteitem> Userfavouriteitems { get; set; }

    public virtual DbSet<Waitingticket> Waitingtickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=SchemaTest;Username=postgres;password=Tatva@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Cityid).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Cityname)
                .HasMaxLength(50)
                .HasColumnName("cityname");
            entity.Property(e => e.Stateid).HasColumnName("stateid");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Stateid)
                .HasConstraintName("city_stateid_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countryid).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Countryname)
                .HasMaxLength(50)
                .HasColumnName("countryname");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Customerid)
                .ValueGeneratedOnAdd()
                .HasColumnName("customerid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.CustomerCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("customer_createdby_fkey");

            entity.HasOne(d => d.CustomerNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.Customerid)
                .HasConstraintName("customer_customerid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.CustomerUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("customer_updatedby_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Itemid).HasName("items_pkey");

            entity.ToTable("items");

            entity.Property(e => e.Itemid)
                .ValueGeneratedOnAdd()
                .HasColumnName("itemid");
            entity.Property(e => e.Available)
                .HasDefaultValueSql("'1'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("available");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Ismodifiable)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("ismodifiable");
            entity.Property(e => e.Itemimage).HasColumnName("itemimage");
            entity.Property(e => e.Itemname)
                .HasMaxLength(50)
                .HasColumnName("itemname");
            entity.Property(e => e.Itemtype)
                .HasMaxLength(50)
                .HasColumnName("itemtype");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.ItemCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("items_createdby_fkey");

            entity.HasOne(d => d.ItemNavigation).WithOne(p => p.Item)
                .HasForeignKey<Item>(d => d.Itemid)
                .HasConstraintName("items_itemid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.ItemUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("items_updatedby_fkey");
        });

        modelBuilder.Entity<Loginuser>(entity =>
        {
            entity.HasKey(e => e.Loginuserid).HasName("loginuser_pkey");

            entity.ToTable("loginuser");

            entity.Property(e => e.Loginuserid).HasColumnName("loginuserid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isfirsttimelogin).HasColumnName("isfirsttimelogin");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.LoginuserCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("loginuser_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.LoginuserUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("loginuser_updatedby_fkey");
        });

        modelBuilder.Entity<Mainnavbar>(entity =>
        {
            entity.HasKey(e => e.Navbaritemid).HasName("mainnavbar_pkey");

            entity.ToTable("mainnavbar");

            entity.Property(e => e.Navbaritemid).HasColumnName("navbaritemid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Navbaritemname).HasColumnName("navbaritemname");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.MainnavbarCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("mainnavbar_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.MainnavbarUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("mainnavbar_updatedby_fkey");
        });

        modelBuilder.Entity<Menucategory>(entity =>
        {
            entity.HasKey(e => e.Menucategoryid).HasName("menucategory_pkey");

            entity.ToTable("menucategory");

            entity.Property(e => e.Menucategoryid).HasColumnName("menucategoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.MenucategoryCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("menucategory_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.MenucategoryUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("menucategory_updatedby_fkey");
        });

        modelBuilder.Entity<Modifiergroup>(entity =>
        {
            entity.HasKey(e => e.Modifiergroupid).HasName("modifiergroup_pkey");

            entity.ToTable("modifiergroup");

            entity.Property(e => e.Modifiergroupid).HasColumnName("modifiergroupid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Menucategoryid).HasColumnName("menucategoryid");
            entity.Property(e => e.Modifiername)
                .HasMaxLength(50)
                .HasColumnName("modifiername");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.ModifiergroupCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("modifiergroup_createdby_fkey");

            entity.HasOne(d => d.Menucategory).WithMany(p => p.Modifiergroups)
                .HasForeignKey(d => d.Menucategoryid)
                .HasConstraintName("modifiergroup_menucategoryid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.ModifiergroupUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("modifiergroup_updatedby_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Admincomment)
                .HasMaxLength(500)
                .HasColumnName("admincomment");
            entity.Property(e => e.Avgrating)
                .HasPrecision(5, 2)
                .HasColumnName("avgrating");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerfeedback)
                .HasMaxLength(500)
                .HasColumnName("customerfeedback");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Paymentmode).HasColumnName("paymentmode");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Tableid).HasColumnName("tableid");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.OrderCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("orders_createdby_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_customerid_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Tableid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_tableid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.OrderUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("orders_updatedby_fkey");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Orderdetailid).HasName("orderdetails_pkey");

            entity.ToTable("orderdetails");

            entity.Property(e => e.Orderdetailid).HasColumnName("orderdetailid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.OrderdetailCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("orderdetails_createdby_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orderdetails_itemid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("orderdetails_orderid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.OrderdetailUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("orderdetails_updatedby_fkey");
        });

        modelBuilder.Entity<Ordermodifierdetail>(entity =>
        {
            entity.HasKey(e => e.Ordermodifierdetailid).HasName("ordermodifierdetails_pkey");

            entity.ToTable("ordermodifierdetails");

            entity.Property(e => e.Ordermodifierdetailid).HasColumnName("ordermodifierdetailid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Orderdetailid).HasColumnName("orderdetailid");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.OrdermodifierdetailCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("ordermodifierdetails_createdby_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.Ordermodifierdetails)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ordermodifierdetails_itemid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.OrdermodifierdetailUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("ordermodifierdetails_updatedby_fkey");
        });

        modelBuilder.Entity<Orderrating>(entity =>
        {
            entity.HasKey(e => e.Orderratingid).HasName("orderratings_pkey");

            entity.ToTable("orderratings");

            entity.Property(e => e.Orderratingid).HasColumnName("orderratingid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Ratingid).HasColumnName("ratingid");
            entity.Property(e => e.Ratingorder).HasColumnName("ratingorder");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.OrderratingCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("orderratings_createdby_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderratings)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderratings_orderid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.OrderratingUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("orderratings_updatedby_fkey");
        });

        modelBuilder.Entity<Orderstatus>(entity =>
        {
            entity.HasKey(e => e.Orderstatusid).HasName("orderstatus_pkey");

            entity.ToTable("orderstatus");

            entity.Property(e => e.Orderstatusid).HasColumnName("orderstatusid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.OrderstatusCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("orderstatus_createdby_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderstatuses)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderstatus_orderid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.OrderstatusUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("orderstatus_updatedby_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Canaddedit)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("canaddedit");
            entity.Property(e => e.Candelete)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("candelete");
            entity.Property(e => e.Canview)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("canview");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Menuid).HasColumnName("menuid");
            entity.Property(e => e.Permissionname)
                .HasMaxLength(50)
                .HasColumnName("permissionname");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.PermissionCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("permissions_createdby_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("permissions_roleid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.PermissionUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("permissions_updatedby_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Rolename, "roles_rolename_key").IsUnique();

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Sectionid).HasName("sections_pkey");

            entity.ToTable("sections");

            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Sectionname)
                .HasMaxLength(50)
                .HasColumnName("sectionname");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.SectionCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("sections_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.SectionUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("sections_updatedby_fkey");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Stateid).HasName("state_pkey");

            entity.ToTable("state");

            entity.Property(e => e.Stateid).HasColumnName("stateid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Statename)
                .HasMaxLength(50)
                .HasColumnName("statename");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("state_countryid_fkey");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Tableid).HasName("tables_pkey");

            entity.ToTable("tables");

            entity.Property(e => e.Tableid).HasColumnName("tableid");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("status");
            entity.Property(e => e.Tablename)
                .HasMaxLength(50)
                .HasColumnName("tablename");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.TableCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("tables_createdby_fkey");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.Sectionid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tables_sectionid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.TableUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("tables_updatedby_fkey");
        });

        modelBuilder.Entity<Tablegrouping>(entity =>
        {
            entity.HasKey(e => e.Tablegroupingid).HasName("tablegrouping_pkey");

            entity.ToTable("tablegrouping");

            entity.Property(e => e.Tablegroupingid).HasColumnName("tablegroupingid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Tableid).HasColumnName("tableid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.TablegroupingCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("tablegrouping_createdby_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Tablegroupings)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("tablegrouping_orderid_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Tablegroupings)
                .HasForeignKey(d => d.Tableid)
                .HasConstraintName("tablegrouping_tableid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.TablegroupingUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("tablegrouping_updatedby_fkey");
        });

        modelBuilder.Entity<Taxesandfee>(entity =>
        {
            entity.HasKey(e => e.Taxid).HasName("taxesandfees_pkey");

            entity.ToTable("taxesandfees");

            entity.Property(e => e.Taxid).HasColumnName("taxid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdefault)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdefault");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isenabled)
                .HasDefaultValueSql("'1'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isenabled");
            entity.Property(e => e.Taxname).HasColumnName("taxname");
            entity.Property(e => e.Taxtype).HasColumnName("taxtype");
            entity.Property(e => e.Taxvalue)
                .HasPrecision(10, 2)
                .HasColumnName("taxvalue");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.TaxesandfeeCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("taxesandfees_createdby_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.TaxesandfeeUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("taxesandfees_updatedby_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(555)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Profile).HasColumnName("profile");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.States)
                .HasMaxLength(50)
                .HasColumnName("states");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_roleid_fkey");
        });

        modelBuilder.Entity<Userfavouriteitem>(entity =>
        {
            entity.HasKey(e => e.Userfavouriteitem1).HasName("userfavouriteitem_pkey");

            entity.ToTable("userfavouriteitem");

            entity.Property(e => e.Userfavouriteitem1).HasColumnName("userfavouriteitem");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.UserfavouriteitemCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("userfavouriteitem_createdby_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.Userfavouriteitems)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("userfavouriteitem_itemid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.UserfavouriteitemUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("userfavouriteitem_updatedby_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserfavouriteitemUsers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("userfavouriteitem_userid_fkey");
        });

        modelBuilder.Entity<Waitingticket>(entity =>
        {
            entity.HasKey(e => e.Waitingticketid).HasName("waitingticket_pkey");

            entity.ToTable("waitingticket");

            entity.Property(e => e.Waitingticketid).HasColumnName("waitingticketid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'::\"bit\"")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.People).HasColumnName("people");
            entity.Property(e => e.Sectionid).HasColumnName("sectionid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.WaitingticketCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .HasConstraintName("waitingticket_createdby_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Waitingtickets)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("waitingticket_customerid_fkey");

            entity.HasOne(d => d.Section).WithMany(p => p.WaitingticketSections)
                .HasForeignKey(d => d.Sectionid)
                .HasConstraintName("waitingticket_sectionid_fkey");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.WaitingticketUpdatedbyNavigations)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("waitingticket_updatedby_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

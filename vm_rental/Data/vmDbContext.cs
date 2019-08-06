using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using vm_rental.Data.Model;

namespace vm_rental.Data
{
    public partial class vmDbContext : DbContext
    {
        public vmDbContext()
        {
        }

        public vmDbContext(DbContextOptions<vmDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientDiscount> ClientDiscount { get; set; }
        public virtual DbSet<ClientDiscountHistory> ClientDiscountHistory { get; set; }
        public virtual DbSet<ClientHistory> ClientHistory { get; set; }
        public virtual DbSet<ComponentType> ComponentType { get; set; }
        public virtual DbSet<ComponentTypeHistory> ComponentTypeHistory { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<MachineComponent> MachineComponent { get; set; }
        public virtual DbSet<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual DbSet<MachineHistory> MachineHistory { get; set; }
        public virtual DbSet<MachinesUsers> MachinesUsers { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductHistory> ProductHistory { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<ProductSupplierHistory> ProductSupplierHistory { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolesPermissions> RolesPermissions { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserHistory> UserHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=admin;database=vm_usage_reports");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("Indx_client_id");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_ID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ClientDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("client_discount", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_Client_Discount_Client1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Client_Discount_Product1_idx");

                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientDiscount)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_Client1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ClientDiscount)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_Product1");
            });

            modelBuilder.Entity<ClientDiscountHistory>(entity =>
            {
                entity.HasKey(e => e.DiscountHistoryId);

                entity.ToTable("client_discount_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Client_Discount_History_User1_idx");

                entity.HasIndex(e => e.DiscountId)
                    .HasName("fk_Client_Discount_History_Client_Discount1_idx");

                entity.HasIndex(e => new { e.CreatedBy, e.Version })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.DiscountHistoryId)
                    .HasColumnName("discount_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountPercent)
                    .HasColumnName("discount_percent")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClientDiscountHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_History_User1");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.ClientDiscountHistory)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_History_Client_Discount1");
            });

            modelBuilder.Entity<ClientHistory>(entity =>
            {
                entity.ToTable("client_history", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_Client_History_Client1_idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Client_history_User1_idx");

                entity.HasIndex(e => new { e.Version, e.CreatedBy })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.ClientHistoryId)
                    .HasColumnName("client_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.FirmEmail)
                    .IsRequired()
                    .HasColumnName("firm_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirmFax)
                    .HasColumnName("firm_fax")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FirmName)
                    .HasColumnName("firm_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirmOwner)
                    .IsRequired()
                    .HasColumnName("firm_owner")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirmPhone)
                    .IsRequired()
                    .HasColumnName("firm_phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FirmRegNumber)
                    .HasColumnName("firm_reg_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsFirm)
                    .HasColumnName("is_firm")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsVatTaxed)
                    .HasColumnName("is_vat_taxed")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.VatNumber)
                    .HasColumnName("vat_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientHistory)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_History_Client1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClientHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_history_User1");
            });

            modelBuilder.Entity<ComponentType>(entity =>
            {
                entity.ToTable("component_type", "vm_usage_reports");

                entity.Property(e => e.ComponentTypeId)
                    .HasColumnName("component_type_ID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ComponentTypeHistory>(entity =>
            {
                entity.ToTable("component_type_history", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentTypeId)
                    .HasName("fk_Component_Type_History_Component_Type1_idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Machine_Component_Type_History_User1_idx");

                entity.HasIndex(e => new { e.CreatedBy, e.Version })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.ComponentTypeHistoryId)
                    .HasColumnName("component_type_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComponentTypeId)
                    .HasColumnName("component_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Splitable)
                    .HasColumnName("splitable")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentType)
                    .WithMany(p => p.ComponentTypeHistory)
                    .HasForeignKey(d => d.ComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Component_Type_History_Component_Type1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ComponentTypeHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_Type_History_User1");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_Machine_Client1_idx");

                entity.HasIndex(e => e.MachineId)
                    .HasName("indx_machine_id_&_version")
                    .IsUnique();

                entity.Property(e => e.MachineId)
                    .HasColumnName("machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Machine)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Client1");
            });

            modelBuilder.Entity<MachineComponent>(entity =>
            {
                entity.HasKey(e => e.ComponentId);

                entity.ToTable("machine_component", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentTypeId)
                    .HasName("fk_Machine_Component_Component_Type1_idx");

                entity.HasIndex(e => e.MachineId)
                    .HasName("fk_Machine_Component_Machine1_idx");

                entity.Property(e => e.ComponentId)
                    .HasColumnName("component_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActiveAmount)
                    .HasColumnName("active_amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.ComponentTypeId)
                    .HasColumnName("component_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MachineId)
                    .HasColumnName("machine_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentType)
                    .WithMany(p => p.MachineComponent)
                    .HasForeignKey(d => d.ComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_Component_Type1");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineComponent)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_Machine1");
            });

            modelBuilder.Entity<MachineComponentHistory>(entity =>
            {
                entity.HasKey(e => e.ComponentHistoryId);

                entity.ToTable("machine_component_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Components_history_User1_idx");

                entity.HasIndex(e => e.MachineComponentId)
                    .HasName("fk_Machine_Component_History_Machine_Component1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Machine_Component_History_Product1_idx");

                entity.HasIndex(e => new { e.CreatedBy, e.Version })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.ComponentHistoryId)
                    .HasColumnName("component_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddedAmount)
                    .HasColumnName("added_amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.MachineComponentId)
                    .HasColumnName("machine_component_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MachineComponentHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Components_history_User1");

                entity.HasOne(d => d.MachineComponent)
                    .WithMany(p => p.MachineComponentHistory)
                    .HasForeignKey(d => d.MachineComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_History_Machine_Component1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MachineComponentHistory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_History_Product1");
            });

            modelBuilder.Entity<MachineHistory>(entity =>
            {
                entity.ToTable("machine_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Machine_History_User1_idx");

                entity.HasIndex(e => e.MachineId)
                    .HasName("fk_Machine_History_Machine1_idx");

                entity.HasIndex(e => new { e.CreatedBy, e.Version })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.MachineHistoryId)
                    .HasColumnName("machine_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.MachineId)
                    .HasColumnName("machine_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MachineHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_History_User1");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineHistory)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_History_Machine1");
            });

            modelBuilder.Entity<MachinesUsers>(entity =>
            {
                entity.ToTable("machines_users", "vm_usage_reports");

                entity.HasIndex(e => e.MachineId)
                    .HasName("fk_Machines_Users_Machine1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_Machines_Users_User1_idx");

                entity.Property(e => e.MachinesUsersId)
                    .HasColumnName("machines_users_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MachineId)
                    .HasColumnName("machine_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachinesUsers)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_Users_Machine1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MachinesUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_Users_User1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order", "vm_usage_reports");

                entity.HasIndex(e => e.OrderedBy)
                    .HasName("fk_Orders_User1_idx");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateOrdered).HasColumnName("date_ordered");

                entity.Property(e => e.OrderedBy)
                    .HasColumnName("ordered_by")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.OrderedByNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_User1");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => e.RecId);

                entity.ToTable("order_items", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentId)
                    .HasName("fk_Order_Items_Machine_Component1_idx");

                entity.HasIndex(e => e.OrderId)
                    .HasName("fk_Order_Items_Order1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Orders_Resources_Product1_idx");

                entity.Property(e => e.RecId)
                    .HasColumnName("rec_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComponentId)
                    .HasColumnName("component_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.EndDateExecuted).HasColumnName("end_date_executed");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasColumnName("order_type")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.OrderedAmount)
                    .HasColumnName("ordered_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.StartDateExecuted).HasColumnName("start_date_executed");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Items_Machine_Component1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Items_Order1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_items_Product1");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission", "vm_usage_reports");

                entity.HasIndex(e => e.PermissionName)
                    .HasName("permission_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HasPermission)
                    .HasColumnName("has_permission")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasColumnName("permission_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentTypeId)
                    .HasName("fk_Product_Component_Type1_idx");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComponentTypeId)
                    .HasColumnName("component_type_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentType)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Component_Type1");
            });

            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("product_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Products_history_User1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Product_History_Product1_idx");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("fk_Product_Product_Supplier_idx");

                entity.HasIndex(e => new { e.Version, e.CreatedBy })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.ProductHistoryId)
                    .HasColumnName("product_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(19,5)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnName("unit")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProductHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Products_history_User1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductHistory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_History_Product1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductHistory)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Products_Product_Supplier10");
            });

            modelBuilder.Entity<ProductSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("product_supplier", "vm_usage_reports");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_ID")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ProductSupplierHistory>(entity =>
            {
                entity.HasKey(e => e.SupplierHistoryId);

                entity.ToTable("product_supplier_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Product_Supplier_history_User1_idx");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("fk_Product_Supplier_history_Product_Supplier1_idx");

                entity.HasIndex(e => new { e.Version, e.CreatedBy })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.SupplierHistoryId)
                    .HasColumnName("supplier_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.SupplierDescription)
                    .HasColumnName("supplier_description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasColumnName("supplier_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierPhone)
                    .HasColumnName("supplier_phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProductSupplierHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Supplier_history_User1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductSupplierHistory)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Supplier_history_Product_Supplier1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role", "vm_usage_reports");

                entity.HasIndex(e => e.MachinesUsersId)
                    .HasName("fk_Role_Machines_Users1_idx");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.MachinesUsersId)
                    .HasColumnName("machines_users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.MachinesUsers)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.MachinesUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Role_Machines_Users1");
            });

            modelBuilder.Entity<RolesPermissions>(entity =>
            {
                entity.HasKey(e => e.RolePermissionId);

                entity.ToTable("roles_permissions", "vm_usage_reports");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("fk_Roles_Permissions_Permission1_idx");

                entity.HasIndex(e => e.RoleId)
                    .HasName("fk_Roles_Permissions_Role1_idx");

                entity.Property(e => e.RolePermissionId)
                    .HasColumnName("role_permission_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolesPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Roles_Permissions_Permission1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolesPermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Roles_Permissions_Role1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_User_Client1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("Indx_user_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Client1");
            });

            modelBuilder.Entity<UserHistory>(entity =>
            {
                entity.ToTable("user_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_User_History_User1_idx");

                entity.HasIndex(e => e.UserHistoryId)
                    .HasName("rec_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.CreatedBy, e.Version })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.UserHistoryId)
                    .HasColumnName("user_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PwdHash)
                    .IsRequired()
                    .HasColumnName("pwd_hash")
                    .HasColumnType("binary(32)");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email")
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasColumnName("user_phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_History_User1");
            });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vm_rental.Data.Model
{
    public partial class vmDbContext : DbContext
    {
        public vmDbContext()
        {
        }

        public vmDbContext(DbContextOptions<vmDbContext> options) : base(options) { }

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
                //http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
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
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ClientDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("client_discount", "vm_usage_reports");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_Client_Discount_Client1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Client_Discount_Product1_idx");

                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.ClientDiscount)
                    .HasForeignKey(d => d.ClientClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_Client1");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.ClientDiscount)
                    .HasForeignKey(d => d.ProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_Product1");
            });

            modelBuilder.Entity<ClientDiscountHistory>(entity =>
            {
                entity.HasKey(e => e.DiscountHistoryId);

                entity.ToTable("client_discount_history", "vm_usage_reports");

                entity.HasIndex(e => e.ClientDiscountDiscountId)
                    .HasName("fk_Client_Discount_History_Client_Discount1_idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Client_Discount_History_User1_idx");

                entity.HasIndex(e => e.Version)
                    .HasName("indx_Client_client_id_&_Product_product_id_&_version")
                    .IsUnique();

                entity.Property(e => e.DiscountHistoryId)
                    .HasColumnName("discount_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientDiscountDiscountId)
                    .HasColumnName("Client_Discount_discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.DiscountPercent)
                    .HasColumnName("discount_percent")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClientDiscountDiscount)
                    .WithMany(p => p.ClientDiscountHistory)
                    .HasForeignKey(d => d.ClientDiscountDiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_History_Client_Discount1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ClientDiscountHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Discount_History_User1");
            });

            modelBuilder.Entity<ClientHistory>(entity =>
            {
                entity.ToTable("client_history", "vm_usage_reports");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_Client_History_Client1_idx");

                entity.HasIndex(e => e.ClientHistoryId)
                    .HasName("rec_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Client_history_User1_idx");

                entity.HasIndex(e => e.Version)
                    .HasName("Indx_client_id_&_version")
                    .IsUnique();

                entity.Property(e => e.ClientHistoryId)
                    .HasColumnName("client_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccPerson)
                    .IsRequired()
                    .HasColumnName("Acc_Person")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.FirmName)
                    .IsRequired()
                    .HasColumnName("firm_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirmNumber)
                    .IsRequired()
                    .HasColumnName("firm_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsVatTaxed)
                    .HasColumnName("is_vat_taxed")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.OrgEmail)
                    .IsRequired()
                    .HasColumnName("org_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrgFax)
                    .HasColumnName("org_fax")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OrgPhone)
                    .IsRequired()
                    .HasColumnName("org_phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.VatNumber)
                    .HasColumnName("vat_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("tinyint(6)");

                entity.Property(e => e.WithoutVarReason)
                    .HasColumnName("without_var_reason")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.ClientHistory)
                    .HasForeignKey(d => d.ClientClientId)
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
                    .HasColumnName("component_type_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ComponentTypeHistory>(entity =>
            {
                entity.ToTable("component_type_history", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentTypeComponentTypeId)
                    .HasName("fk_Component_Type_History_Component_Type1_idx");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Machine_Component_Type_History_User1_idx");

                entity.Property(e => e.ComponentTypeHistoryId)
                    .HasColumnName("component_type_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComponentTypeComponentTypeId)
                    .HasColumnName("Component_Type_component_type_id")
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
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentTypeComponentType)
                    .WithMany(p => p.ComponentTypeHistory)
                    .HasForeignKey(d => d.ComponentTypeComponentTypeId)
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

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_Machine_Client1_idx");

                entity.HasIndex(e => e.MachineId)
                    .HasName("indx_machine_id_&_version")
                    .IsUnique();

                entity.Property(e => e.MachineId)
                    .HasColumnName("machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("Client_client_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.Machine)
                    .HasForeignKey(d => d.ClientClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Client1");
            });

            modelBuilder.Entity<MachineComponent>(entity =>
            {
                entity.HasKey(e => e.ComponentId);

                entity.ToTable("machine_component", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentTypeComponentTypeId)
                    .HasName("fk_Machine_Component_Component_Type1_idx");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machine_Component_Machine1_idx");

                entity.Property(e => e.ComponentId)
                    .HasColumnName("component_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActiveAmount)
                    .HasColumnName("active_amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.ComponentTypeComponentTypeId)
                    .HasColumnName("Component_Type_component_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentTypeComponentType)
                    .WithMany(p => p.MachineComponent)
                    .HasForeignKey(d => d.ComponentTypeComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_Component_Type1");

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachineComponent)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_Machine1");
            });

            modelBuilder.Entity<MachineComponentHistory>(entity =>
            {
                entity.HasKey(e => e.ComponentHistoryId);

                entity.ToTable("machine_component_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Components_history_User1_idx");

                entity.HasIndex(e => e.MachineComponentComponentId)
                    .HasName("fk_Machine_Component_History_Machine_Component1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Machine_Component_History_Product1_idx");

                entity.Property(e => e.ComponentHistoryId)
                    .HasColumnName("component_history_id")
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
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.MachineComponentComponentId)
                    .HasColumnName("Machine_Component_component_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MachineComponentHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Components_history_User1");

                entity.HasOne(d => d.MachineComponentComponent)
                    .WithMany(p => p.MachineComponentHistory)
                    .HasForeignKey(d => d.MachineComponentComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_History_Machine_Component1");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.MachineComponentHistory)
                    .HasForeignKey(d => d.ProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_History_Product1");
            });

            modelBuilder.Entity<MachineHistory>(entity =>
            {
                entity.ToTable("machine_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Machine_History_User1_idx");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machine_History_Machine1_idx");

                entity.HasIndex(e => e.Version)
                    .HasName("indx_machine_id_&_version")
                    .IsUnique();

                entity.Property(e => e.MachineHistoryId)
                    .HasColumnName("machine_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
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

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachineHistory)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_History_Machine1");
            });

            modelBuilder.Entity<MachinesUsers>(entity =>
            {
                entity.ToTable("machines_users", "vm_usage_reports");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machines_Users_Machine1_idx");

                entity.HasIndex(e => e.UserUserId)
                    .HasName("fk_Machines_Users_User1_idx");

                entity.Property(e => e.MachinesUsersId)
                    .HasColumnName("machines_users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserUserId)
                    .HasColumnName("User_user_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachinesUsers)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_Users_Machine1");

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.MachinesUsers)
                    .HasForeignKey(d => d.UserUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_Users_User1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedById)
                    .HasName("fk_Orders_User1_idx");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_User1");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => e.RecId);

                entity.ToTable("order_items", "vm_usage_reports");

                entity.HasIndex(e => e.MachineComponentComponentId)
                    .HasName("fk_Order_Items_Machine_Component1_idx");

                entity.HasIndex(e => e.OrderOrderId)
                    .HasName("fk_Order_Items_Order1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Orders_Resources_Product1_idx");

                entity.Property(e => e.RecId)
                    .HasColumnName("rec_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.EndDateExecuted).HasColumnName("end_date_executed");

                entity.Property(e => e.MachineComponentComponentId)
                    .HasColumnName("Machine_Component_component_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderOrderId)
                    .HasColumnName("Order_order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasColumnName("order_type")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.OrderedAmount)
                    .HasColumnName("ordered_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.StartDateExecuted).HasColumnName("start_date_executed");

                entity.HasOne(d => d.MachineComponentComponent)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MachineComponentComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Items_Machine_Component1");

                entity.HasOne(d => d.OrderOrder)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Items_Order1");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductProductId)
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

                entity.HasIndex(e => e.ComponentTypeComponentTypeId)
                    .HasName("fk_Product_Component_Type1_idx");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComponentTypeComponentTypeId)
                    .HasColumnName("Component_Type_component_type_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentTypeComponentType)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ComponentTypeComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Component_Type1");
            });

            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("product_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Products_history_User1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Product_History_Product1_idx");

                entity.HasIndex(e => e.ProductSupplierSupplierId)
                    .HasName("fk_Product_Product_Supplier_idx");

                entity.HasIndex(e => e.Version)
                    .HasName("indx_product_id_&_version")
                    .IsUnique();

                entity.Property(e => e.ProductHistoryId)
                    .HasColumnName("product_history_id")
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

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductSupplierSupplierId)
                    .HasColumnName("Product_Supplier_supplier_id")
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

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.ProductHistory)
                    .HasForeignKey(d => d.ProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_History_Product1");

                entity.HasOne(d => d.ProductSupplierSupplier)
                    .WithMany(p => p.ProductHistory)
                    .HasForeignKey(d => d.ProductSupplierSupplierId)
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

                entity.HasIndex(e => e.ProductSupplierSupplierId)
                    .HasName("fk_Product_Supplier_history_Product_Supplier1_idx");

                entity.HasIndex(e => e.Version)
                    .HasName("indx_supplier_id_&_version")
                    .IsUnique();

                entity.Property(e => e.SupplierHistoryId)
                    .HasColumnName("supplier_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ProductSupplierSupplierId)
                    .HasColumnName("Product_Supplier_supplier_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierDescription)
                    .HasColumnName("supplier_description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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

                entity.HasOne(d => d.ProductSupplierSupplier)
                    .WithMany(p => p.ProductSupplierHistory)
                    .HasForeignKey(d => d.ProductSupplierSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Supplier_history_Product_Supplier1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role", "vm_usage_reports");

                entity.HasIndex(e => e.MachinesUsersMachinesUsersId)
                    .HasName("fk_Role_Machines_Users1_idx");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.MachinesUsersMachinesUsersId)
                    .HasColumnName("Machines_Users_machines_users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.MachinesUsersMachinesUsers)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.MachinesUsersMachinesUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Role_Machines_Users1");
            });

            modelBuilder.Entity<RolesPermissions>(entity =>
            {
                entity.HasKey(e => e.RolePermissionId);

                entity.ToTable("roles_permissions", "vm_usage_reports");

                entity.HasIndex(e => e.PermissionPermissionId)
                    .HasName("fk_Roles_Permissions_Permission1_idx");

                entity.HasIndex(e => e.RoleRoleId)
                    .HasName("fk_Roles_Permissions_Role1_idx");

                entity.Property(e => e.RolePermissionId)
                    .HasColumnName("role_permission_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PermissionPermissionId)
                    .HasColumnName("Permission_permission_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleRoleId)
                    .HasColumnName("Role_role_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.PermissionPermission)
                    .WithMany(p => p.RolesPermissions)
                    .HasForeignKey(d => d.PermissionPermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Roles_Permissions_Permission1");

                entity.HasOne(d => d.RoleRole)
                    .WithMany(p => p.RolesPermissions)
                    .HasForeignKey(d => d.RoleRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Roles_Permissions_Role1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "vm_usage_reports");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_User_Client1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("Indx_user_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("Client_client_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ClientClientId)
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

                entity.HasIndex(e => e.Version)
                    .HasName("Indx_user_id_&_version")
                    .IsUnique();

                entity.Property(e => e.UserHistoryId)
                    .HasColumnName("user_history_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(15)
                    .IsUnicode(false);

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

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PwdHash)
                    .IsRequired()
                    .HasColumnName("pwd_hash")
                    .HasColumnType("binary(32)")
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
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

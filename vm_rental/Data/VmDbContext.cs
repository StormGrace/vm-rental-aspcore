using Microsoft.EntityFrameworkCore;
using System;
using vm_rental.Data.Model;

namespace vm_rental.Data
{
    public partial class VmDbContext
    {
        public VmDbContext(){}
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientDiscount> ClientDiscount { get; set; }
        public virtual DbSet<ClientDiscountHistory> ClientDiscountHistory { get; set; }
        public virtual DbSet<ClientHistory> ClientHistory { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<MachineComponent> MachineComponent { get; set; }
        public virtual DbSet<MachineComponentHistory> MachineComponentHistory { get; set; }
        public virtual DbSet<MachineComponentType> MachineComponentType { get; set; }
        public virtual DbSet<MachineComponentTypeHistory> MachineComponentTypeHistory { get; set; }
        public virtual DbSet<MachineHistory> MachineHistory { get; set; }
        public virtual DbSet<MachinesUsers> MachinesUsers { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrdersItems> OrdersItems { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductHistory> ProductHistory { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<ProductSupplierHistory> ProductSupplierHistory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserHistory> UserHistory { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleClaim> UserRoleClaim { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<UsersRoles> UsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            //Identity Framework Ignores
            modelBuilder.Entity<User>().Ignore(u => u.NormalizedEmail);
            modelBuilder.Entity<User>().Ignore(u => u.NormalizedUserName);
            modelBuilder.Entity<User>().Ignore(u => u.PhoneNumberConfirmed);
            modelBuilder.Entity<User>().Ignore(u => u.TwoFactorEnabled);
            //

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("Indx_client_id");

                entity.HasIndex(e => e.FirmEmail)
                    .HasName("firm_email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FirmEmail)
                    .IsRequired()
                    .HasColumnName("firm_email")
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmFax)
                    .HasColumnName("firm_fax")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmName)
                    .HasColumnName("firm_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmOwner)
                    .IsRequired()
                    .HasColumnName("firm_owner")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmPhone)
                    .IsRequired()
                    .HasColumnName("firm_phone")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmRegNumber)
                    .HasColumnName("firm_reg_number")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.IsFirm)
                    .HasColumnName("is_firm")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsVatTaxed)
                    .HasColumnName("is_vat_taxed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.VatNumber)
                    .HasColumnName("vat_number")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");
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

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.DiscountPercent)
                    .HasColumnName("discount_percent")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

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

                entity.HasIndex(e => e.Version)
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.DiscountHistoryId)
                    .HasColumnName("discount_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FirmEmail)
                    .IsRequired()
                    .HasColumnName("firm_email")
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmFax)
                    .HasColumnName("firm_fax")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmName)
                    .HasColumnName("firm_name")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmOwner)
                    .IsRequired()
                    .HasColumnName("firm_owner")
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmPhone)
                    .IsRequired()
                    .HasColumnName("firm_phone")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirmRegNumber)
                    .HasColumnName("firm_reg_number")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.IsFirm)
                    .HasColumnName("is_firm")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsVatTaxed)
                    .HasColumnName("is_vat_taxed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.VatNumber)
                    .HasColumnName("vat_number")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

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

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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
                    .HasMaxLength(45)
                    .IsUnicode(false);

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

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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

            modelBuilder.Entity<MachineComponentType>(entity =>
            {
                entity.HasKey(e => e.ComponentTypeId);

                entity.ToTable("machine_component_type", "vm_usage_reports");

                entity.Property(e => e.ComponentTypeId)
                    .HasColumnName("component_type_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.IsSplitable)
                    .HasColumnName("is_splitable")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MachineComponentTypeHistory>(entity =>
            {
                entity.HasKey(e => e.ComponentTypeHistoryId);

                entity.ToTable("machine_component_type_history", "vm_usage_reports");

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

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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

                entity.Property(e => e.IsSplitable)
                    .HasColumnName("is_splitable")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ComponentType)
                    .WithMany(p => p.MachineComponentTypeHistory)
                    .HasForeignKey(d => d.ComponentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Component_Type_History_Component_Type1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MachineComponentTypeHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Component_Type_History_User1");
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

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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

            modelBuilder.Entity<OrdersItems>(entity =>
            {
                entity.ToTable("orders_items", "vm_usage_reports");

                entity.HasIndex(e => e.ComponentId)
                    .HasName("fk_Order_Items_Machine_Component1_idx");

                entity.HasIndex(e => e.OrderId)
                    .HasName("fk_Order_Items_Order1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Orders_Resources_Product1_idx");

                entity.Property(e => e.OrdersItemsId)
                    .HasColumnName("orders_items_ID")
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
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Items_Machine_Component1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Items_Order1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_items_Product1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product", "vm_usage_reports");

                entity.HasIndex(e => e.Code)
                    .HasName("code_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ComponentTypeId)
                    .HasName("fk_Product_Component_Type1_idx");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentTypeId)
                    .HasColumnName("component_type_id")
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

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnName("unit")
                    .HasMaxLength(15)
                    .IsUnicode(false);

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

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)");

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
            });

            modelBuilder.Entity<ProductSupplierHistory>(entity =>
            {
                entity.HasKey(e => e.SupplierHistoryId);

                entity.ToTable("product_supplier_history", "vm_usage_reports");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("fk_Product_Supplier_history_User1_idx");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("fk_Product_Supplier_History_Product_Supplier1_idx");

                entity.HasIndex(e => new { e.Version, e.CreatedBy })
                    .HasName("Indx_created_by_id_&_version")
                    .IsUnique();

                entity.Property(e => e.SupplierHistoryId)
                    .HasColumnName("supplier_history_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

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
                    .HasConstraintName("fk_Product_Supplier_History_Product_Supplier1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "vm_usage_reports");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_User_Client1_idx");

                entity.HasIndex(e => e.Email)
                    .HasName("email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("Indx_user_id");

                entity.HasIndex(e => e.UserName)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("user_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccessFailedCount)
                    .HasColumnName("access_failed_count")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConcurrencyStamp)
                    .HasColumnName("concurrency_stamp")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.EmailConfirmed)
                    .HasColumnName("email_confirmed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.LockoutEnabled)
                    .HasColumnName("lockout_enabled")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

              entity.Property(e => e.LockoutEnd)
                  .HasColumnName("lockout_end");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(400)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.SecurityStamp)
                    .HasColumnName("security_stamp")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Client1");
            });


            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable("user_claim", "vm_usage_reports");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_User_Claim_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClaimType)
                    .IsRequired()
                    .HasColumnName("claim_type")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimValue)
                    .IsRequired()
                    .HasColumnName("claim_value")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaim)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Claim_User1");
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

                entity.Property(e => e.Changes)
                    .IsRequired()
                    .HasColumnName("changes")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(400)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("N/A");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserHistory)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_History_User1");
            });
            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("user_login", "vm_usage_reports");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_User_Login_User1_idx");

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("login_provier_&_provider_key")
                    .IsUnique();

                entity.Property(e => e.UserLoginId)
                    .HasColumnName("user_login_id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasColumnName("login_provider")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderDisplayName)
                    .HasColumnName("provider_display_name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderKey)
                    .IsRequired()
                    .HasColumnName("provider_key")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogin)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Login_User1");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role", "vm_usage_reports");

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("NormalizedName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ConcurrencyStamp)
                    .HasColumnName("concurrency_stamp")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasColumnName("normalized_name")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRoleClaim>(entity =>
            {
                entity.ToTable("user_role_claim", "vm_usage_reports");

                entity.HasIndex(e => e.RoleId)
                    .HasName("fk_Role_Claims_Roles1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClaimType)
                    .HasColumnName("claim_type")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimValue)
                    .HasColumnName("claim_value")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleClaim)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Role_Claims_Roles1");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("user_token", "vm_usage_reports");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_User_Token_User1_idx");

                entity.HasIndex(e => new { e.LoginProvider, e.Name, e.Value })
                    .HasName("login_provier_&_name_&_value")
                    .IsUnique();

                entity.Property(e => e.UserTokenId)
                    .HasColumnName("user_token_id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasColumnName("login_provider")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Token_User1");
            });

            modelBuilder.Entity<UsersRoles>(entity =>
            {
                entity.ToTable("users_roles", "vm_usage_reports");

                entity.HasIndex(e => e.RoleId)
                    .HasName("fk_User_Roles_Roles1_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_Users_Roles_User1_idx");

                entity.Property(e => e.UsersRolesId)
                    .HasColumnName("users_roles_ID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsersRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Roles_Roles1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Users_Roles_User1");
            });
        }
    }
}

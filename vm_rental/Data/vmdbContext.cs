using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vm_rental.Data.Models
{
    public partial class vmdbContext : DbContext
    {
        public vmdbContext()
        {
        }

        public vmdbContext(DbContextOptions<vmdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLog> AdminLog { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientDiscount> ClientDiscount { get; set; }
        public virtual DbSet<ClientOrder> ClientOrder { get; set; }
        public virtual DbSet<ClientPayment> ClientPayment { get; set; }
        public virtual DbSet<GroupsPermissions> GroupsPermissions { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<MachineInvoiceLog> MachineInvoiceLog { get; set; }
        public virtual DbSet<MachinePanelLog> MachinePanelLog { get; set; }
        public virtual DbSet<MachineResource> MachineResource { get; set; }
        public virtual DbSet<MachineResourceLog> MachineResourceLog { get; set; }
        public virtual DbSet<MachinesUserGroups> MachinesUserGroups { get; set; }
        public virtual DbSet<MachinesUsers> MachinesUsers { get; set; }
        public virtual DbSet<OrdersResources> OrdersResources { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductSupplier> ProductSupplier { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<ResourceUsageLog> ResourceUsageLog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserPermission> UserPermission { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=admin;database=vmdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AdminLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("admin_log", "vmdb");

                entity.Property(e => e.LogId)
                    .HasColumnName("logID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasColumnName("event")
                    .HasColumnType("char(6)");

                entity.Property(e => e.RecordId)
                    .HasColumnName("record_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RecordName)
                    .IsRequired()
                    .HasColumnName("record_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.RecordValueNew)
                    .IsRequired()
                    .HasColumnName("record_value_new")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RecordValueOld)
                    .IsRequired()
                    .HasColumnName("record_value_old")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnName("table_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TimeLogged).HasColumnName("time_logged");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "vmdb");

                entity.HasIndex(e => e.UserName)
                    .HasName("user_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FirmName)
                    .HasColumnName("firm_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirmOwner)
                    .HasColumnName("firm_owner")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsFirm)
                    .HasColumnName("is_firm")
                    .HasColumnType("tinyint(4)");

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
                    .HasColumnType("binary(64)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientDiscount>(entity =>
            {
                entity.ToTable("client_discount", "vmdb");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_Client_Discount_Client1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Client_Discount_Product1_idx");

                entity.Property(e => e.ClientDiscountId)
                    .HasColumnName("client_discount_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("Client_client_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountAmount)
                    .IsRequired()
                    .HasColumnName("discount_amount")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IsDiscountActive)
                    .HasColumnName("is_discount_active")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_ID")
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

            modelBuilder.Entity<ClientOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("client_order", "vmdb");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_Client_Order_Client1_idx");

                entity.HasIndex(e => e.ClientPaymentPaymentId)
                    .HasName("fk_Client_Order_Client_Payment1_idx");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("Client_client_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientPaymentPaymentId)
                    .HasColumnName("Client_Payment_payment_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExecutionDate).HasColumnName("execution_date");

                entity.Property(e => e.IsPrepaid)
                    .HasColumnName("is_prepaid")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.OrderDate).HasColumnName("order_date");

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasColumnName("order_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.ClientOrder)
                    .HasForeignKey(d => d.ClientClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Order_Client1");

                entity.HasOne(d => d.ClientPaymentPayment)
                    .WithMany(p => p.ClientOrder)
                    .HasForeignKey(d => d.ClientPaymentPaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Order_Client_Payment1");
            });

            modelBuilder.Entity<ClientPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("client_payment", "vmdb");

                entity.HasIndex(e => e.MachineInvoiceLogInvoiceId)
                    .HasName("fk_Client_Payment_Machine_Invoice_Log1_idx");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("payment_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateOfPayment).HasColumnName("date_of_payment");

                entity.Property(e => e.DueDate).HasColumnName("due_date");

                entity.Property(e => e.IsScheduled)
                    .HasColumnName("is_scheduled")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.MachineInvoiceLogInvoiceId)
                    .HasColumnName("Machine_Invoice_Log_invoice_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasColumnName("payment_status")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.MachineInvoiceLogInvoice)
                    .WithMany(p => p.ClientPayment)
                    .HasForeignKey(d => d.MachineInvoiceLogInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Client_Payment_Machine_Invoice_Log1");
            });

            modelBuilder.Entity<GroupsPermissions>(entity =>
            {
                entity.HasKey(e => e.GroupPermissionId);

                entity.ToTable("groups_permissions", "vmdb");

                entity.HasIndex(e => e.UserGroupUserGroupId)
                    .HasName("fk_Groups_Permissions_User_Group1_idx");

                entity.HasIndex(e => e.UserPermissionPermissionId)
                    .HasName("fk_Groups_Permissions_User_Permission1_idx");

                entity.Property(e => e.GroupPermissionId)
                    .HasColumnName("group_permission_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserGroupUserGroupId)
                    .HasColumnName("User_Group_user_group_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserPermissionPermissionId)
                    .HasColumnName("User_Permission_permission_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.UserGroupUserGroup)
                    .WithMany(p => p.GroupsPermissions)
                    .HasForeignKey(d => d.UserGroupUserGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Groups_Permissions_User_Group1");

                entity.HasOne(d => d.UserPermissionPermission)
                    .WithMany(p => p.GroupsPermissions)
                    .HasForeignKey(d => d.UserPermissionPermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Groups_Permissions_User_Permission1");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.ToTable("invoice_item", "vmdb");

                entity.HasIndex(e => e.MachineInvoiceLogInvoiceId)
                    .HasName("fk_Invoice_Item_Machine_Invoice_Log1_idx");

                entity.Property(e => e.InvoiceItemId)
                    .HasColumnName("invoice_item_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemAmount)
                    .HasColumnName("item_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemDiscount).HasColumnName("item_discount");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("item_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTotalPrice).HasColumnName("item_total_price");

                entity.Property(e => e.ItemUnit)
                    .IsRequired()
                    .HasColumnName("item_unit")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ItemUnitPrice).HasColumnName("item_unit_price");

                entity.Property(e => e.MachineInvoiceLogInvoiceId)
                    .HasColumnName("Machine_Invoice_Log_invoice_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.MachineInvoiceLogInvoice)
                    .WithMany(p => p.InvoiceItem)
                    .HasForeignKey(d => d.MachineInvoiceLogInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Invoice_Item_Machine_Invoice_Log1");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine", "vmdb");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_Machine_Client1_idx");

                entity.HasIndex(e => e.MachineName)
                    .HasName("machine_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MachineId)
                    .HasColumnName("machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("Client_client_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.IsMachineActive).HasColumnName("is_machine_active");

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasColumnName("machine_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.Machine)
                    .HasForeignKey(d => d.ClientClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Client1");
            });

            modelBuilder.Entity<MachineInvoiceLog>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.ToTable("machine_invoice_log", "vmdb");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machine_Invoice_Log_Machine1_idx");

                entity.Property(e => e.InvoiceId)
                    .HasColumnName("invoice_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountablePerson)
                    .IsRequired()
                    .HasColumnName("accountable_person")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("date");

                entity.Property(e => e.DueDate)
                    .HasColumnName("due_date")
                    .HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasColumnName("invoice_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.IsBankPayment)
                    .HasColumnName("is_bank_payment")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.IsPrepaid)
                    .HasColumnName("is_prepaid")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReceiverAddress)
                    .IsRequired()
                    .HasColumnName("receiver_address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverCity)
                    .IsRequired()
                    .HasColumnName("receiver_city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverName)
                    .IsRequired()
                    .HasColumnName("receiver_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverNumber)
                    .IsRequired()
                    .HasColumnName("receiver_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverVatNumber)
                    .HasColumnName("receiver_vat_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachineInvoiceLog)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Invoice_Log_Machine1");
            });

            modelBuilder.Entity<MachinePanelLog>(entity =>
            {
                entity.HasKey(e => e.PanelLogId);

                entity.ToTable("machine_panel_log", "vmdb");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machine_Panel_Log_Machine1_idx");

                entity.Property(e => e.PanelLogId)
                    .HasColumnName("panel_log_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EventDate).HasColumnName("event_date");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("event_description")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("event_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachinePanelLog)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Panel_Log_Machine1");
            });

            modelBuilder.Entity<MachineResource>(entity =>
            {
                entity.ToTable("machine_resource", "vmdb");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machine_Resource_Machine1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Machine_Resource_Product1_idx");

                entity.Property(e => e.MachineResourceId)
                    .HasColumnName("machine_resource_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsPartition)
                    .HasColumnName("is_partition")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PartitionLabel)
                    .HasColumnName("partition_label")
                    .HasColumnType("char(1)");

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachineResource)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Resource_Machine1");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.MachineResource)
                    .HasForeignKey(d => d.ProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Resource_Product1");
            });

            modelBuilder.Entity<MachineResourceLog>(entity =>
            {
                entity.HasKey(e => e.ResourceLogId);

                entity.ToTable("machine_resource_log", "vmdb");

                entity.HasIndex(e => e.DateAdded)
                    .HasName("update_date_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machine_Resource_Log_Machine1_idx");

                entity.Property(e => e.ResourceLogId)
                    .HasColumnName("resource_log_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateAdded).HasColumnName("date_added");

                entity.Property(e => e.MachineMachineId)
                    .HasColumnName("Machine_machine_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResourceName)
                    .IsRequired()
                    .HasColumnName("resource_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ResourceType)
                    .IsRequired()
                    .HasColumnName("resource_type")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateType)
                    .IsRequired()
                    .HasColumnName("update_type")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.MachineMachine)
                    .WithMany(p => p.MachineResourceLog)
                    .HasForeignKey(d => d.MachineMachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machine_Resource_Log_Machine1");
            });

            modelBuilder.Entity<MachinesUserGroups>(entity =>
            {
                entity.HasKey(e => e.MachineUserGroupId);

                entity.ToTable("machines_user_groups", "vmdb");

                entity.HasIndex(e => e.MachinesUsersMachineUserId)
                    .HasName("fk_Machines_User_Groups_Machines_Users1_idx");

                entity.HasIndex(e => e.UserGroupUserGroupId)
                    .HasName("fk_Machines_User_Groups_User_Group1_idx");

                entity.Property(e => e.MachineUserGroupId)
                    .HasColumnName("machine_user_group_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MachinesUsersMachineUserId)
                    .HasColumnName("Machines_Users_machine_user_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserGroupUserGroupId)
                    .HasColumnName("User_Group_user_group_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.MachinesUsersMachineUser)
                    .WithMany(p => p.MachinesUserGroups)
                    .HasForeignKey(d => d.MachinesUsersMachineUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_User_Groups_Machines_Users1");

                entity.HasOne(d => d.UserGroupUserGroup)
                    .WithMany(p => p.MachinesUserGroups)
                    .HasForeignKey(d => d.UserGroupUserGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Machines_User_Groups_User_Group1");
            });

            modelBuilder.Entity<MachinesUsers>(entity =>
            {
                entity.HasKey(e => e.MachineUserId);

                entity.ToTable("machines_users", "vmdb");

                entity.HasIndex(e => e.MachineMachineId)
                    .HasName("fk_Machines_Users_Machine1_idx");

                entity.HasIndex(e => e.UserUserId)
                    .HasName("fk_Machines_Users_User1_idx");

                entity.Property(e => e.MachineUserId)
                    .HasColumnName("machine_user_ID")
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

            modelBuilder.Entity<OrdersResources>(entity =>
            {
                entity.HasKey(e => e.OrderResourceId);

                entity.ToTable("orders_resources", "vmdb");

                entity.HasIndex(e => e.ClientOrderOrderId)
                    .HasName("fk_Orders_Resources_Client_Order1_idx");

                entity.HasIndex(e => e.ProductProductId)
                    .HasName("fk_Orders_Resources_Product1_idx");

                entity.Property(e => e.OrderResourceId)
                    .HasColumnName("order_resource_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientOrderOrderId)
                    .HasColumnName("Client_Order_order_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductProductId)
                    .HasColumnName("Product_product_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClientOrderOrder)
                    .WithMany(p => p.OrdersResources)
                    .HasForeignKey(d => d.ClientOrderOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_Resources_Client_Order1");

                entity.HasOne(d => d.ProductProduct)
                    .WithMany(p => p.OrdersResources)
                    .HasForeignKey(d => d.ProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Orders_Resources_Product1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product", "vmdb");

                entity.HasIndex(e => e.ProductSupplierSupplierId)
                    .HasName("fk_Product_Product_Supplier1_idx");

                entity.HasIndex(e => e.ProductTypeProductTypeId1)
                    .HasName("fk_Product_Product_Type1_idx");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsAvailable)
                    .HasColumnName("is_available")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasColumnName("product_code")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("product_description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMeasureUnit)
                    .IsRequired()
                    .HasColumnName("product_measure_unit")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.ProductSupplierSupplierId)
                    .HasColumnName("Product_Supplier_supplier_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductTypeProductTypeId1)
                    .HasColumnName("Product_Type_product_type_ID1")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ProductSupplierSupplier)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductSupplierSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Product_Supplier1");

                entity.HasOne(d => d.ProductTypeProductTypeId1Navigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductTypeProductTypeId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Product_Type1");
            });

            modelBuilder.Entity<ProductSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("product_supplier", "vmdb");

                entity.HasIndex(e => e.SupplierName)
                    .HasName("supplier_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierDescription)
                    .HasColumnName("supplier_description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasColumnName("supplier_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierPhone)
                    .HasColumnName("supplier_phone")
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("product_type", "vmdb");

                entity.HasIndex(e => e.ProductTypeName)
                    .HasName("type_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ProductTypeId)
                    .HasColumnName("product_type_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductTypeName)
                    .IsRequired()
                    .HasColumnName("product_type_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ResourceUsageLog>(entity =>
            {
                entity.HasKey(e => e.ResourceUsageId);

                entity.ToTable("resource_usage_log", "vmdb");

                entity.HasIndex(e => e.MachineResourceMachineResourceId)
                    .HasName("fk_Resource_Usage_Period_Machine_Resource1_idx");

                entity.Property(e => e.ResourceUsageId)
                    .HasColumnName("resource_usage_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AmountUsed)
                    .IsRequired()
                    .HasColumnName("amount_used")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.MachineResourceMachineResourceId)
                    .HasColumnName("Machine_Resource_machine_resource_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Usage)
                    .HasColumnName("usage")
                    .HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.MachineResourceMachineResource)
                    .WithMany(p => p.ResourceUsageLog)
                    .HasForeignKey(d => d.MachineResourceMachineResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Resource_Usage_Period_Machine_Resource1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", "vmdb");

                entity.HasIndex(e => e.ClientClientId)
                    .HasName("fk_User_Client1_idx");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientClientId)
                    .HasColumnName("Client_client_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.PwdHash)
                    .IsRequired()
                    .HasColumnName("pwd_hash")
                    .HasColumnType("binary(64)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientClient)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ClientClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Client1");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("user_group", "vmdb");

                entity.Property(e => e.UserGroupId)
                    .HasColumnName("user_group_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("group_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId);

                entity.ToTable("user_permission", "vmdb");

                entity.HasIndex(e => e.PermissionName)
                    .HasName("permission_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HasPermission)
                    .HasColumnName("has_permission")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasColumnName("permission_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}

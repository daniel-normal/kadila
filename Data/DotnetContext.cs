using System;
using System.Collections.Generic;
using kadila.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace kadila.Data;

public partial class DotnetContext : DbContext
{
    public DotnetContext()
    {
    }

    public DotnetContext(DbContextOptions<DotnetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Debt> Debts { get; set; }

    public virtual DbSet<Lot> Lots { get; set; }

    public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    public DbSet<Contact> Contacts { get; set; }
    
    public virtual DbSet<Session> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=dotnet;user=root", ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("customers")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("apellido");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasDefaultValueSql("''")
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
            entity.Property(e => e.SaldoDeuda)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("saldo_deuda");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .HasDefaultValueSql("''")
                .HasColumnName("telefono");
            entity.Property(e => e.TipoCliente)
                .HasColumnType("enum('Persona','Empresa')")
                .HasColumnName("tipo_cliente");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Debt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("debts")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClienteId, "debts_cliente_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ClienteId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("cliente_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('Pendiente','Pagado')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaLimite).HasColumnName("fecha_limite");
            entity.Property(e => e.Monto)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("monto");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Debts)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("debts_cliente_id_foreign");
        });

        modelBuilder.Entity<Lot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("lots")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ProductoId, "lots_producto_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("cantidad");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioActual)
                .HasDefaultValueSql("'12'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("precio_actual");
            entity.Property(e => e.ProductoId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("producto_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Producto).WithMany(p => p.Lots)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("lots_producto_id_foreign");
        });

        modelBuilder.Entity<PaymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("payment_histories")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.DeudaId, "payment_histories_deuda_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeudaId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("deuda_id");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.Monto)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("monto");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Deuda).WithMany(p => p.PaymentHistories)
                .HasForeignKey(d => d.DeudaId)
                .HasConstraintName("payment_histories_deuda_id_foreign");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("products")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasDefaultValueSql("''")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("precio");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => new { e.Name, e.GuardName }, "roles_name_guard_name_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.GuardName).HasColumnName("guard_name");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sales")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClienteId, "sales_cliente_id_foreign");

            entity.HasIndex(e => e.DeudaId, "sales_deuda_id_foreign");

            entity.HasIndex(e => e.EmpleadoId, "sales_empleado_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ClienteId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("cliente_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeudaId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("deuda_id");
            entity.Property(e => e.EmpleadoId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("empleado_id");
            entity.Property(e => e.FechaVenta).HasColumnName("fecha_venta");
            entity.Property(e => e.TipoVenta)
                .HasColumnType("enum('Contado','Cuotas')")
                .HasColumnName("tipo_venta");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("sales_cliente_id_foreign");

            entity.HasOne(d => d.Deuda).WithMany(p => p.Sales)
                .HasForeignKey(d => d.DeudaId)
                .HasConstraintName("sales_deuda_id_foreign");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Sales)
                .HasForeignKey(d => d.EmpleadoId)
                .HasConstraintName("sales_empleado_id_foreign");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sale_details")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ProductoId, "sale_details_producto_id_foreign");

            entity.HasIndex(e => e.VentaId, "sale_details_venta_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Precio)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20)")
                .HasColumnName("precio");
            entity.Property(e => e.ProductoId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("producto_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.VentaId)
                .HasDefaultValueSql("'1'")
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("venta_id");

            entity.HasOne(d => d.Producto).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("sale_details_producto_id_foreign");

            entity.HasOne(d => d.Venta).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("sale_details_venta_id_foreign");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("users")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.RolId, "fk_rol");

            entity.HasIndex(e => e.Email, "users_email_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RolId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("rol_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Rol).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_rol");
        });
        
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.UserId, "sessions_user_id_foreign");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.LoginDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("login_date");
            entity.Property(e => e.LogoutDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("logout_date");
            entity.Property(e => e.UserId)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sessions_user_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public User ValidateUser(string email, string password)
    {
        User user = Users.FirstOrDefault(u => u.Email == email);

        if (user != null)
        {
            var passwordHasher = new PasswordHasher<User>();

            if (user.Password != null)
            {
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }
            }
        }
        return null;
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
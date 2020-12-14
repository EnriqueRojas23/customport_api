
using Api.Data.Mappings.Mantenimiento;
using Api.Domain.Mantenimiento;
using Api.Domain.Seguridad;
using API.Data.Mappings.Seguimiento;
using API.Domain.Seguimiento;
using CargaClic.Data.Mappings.Seguridad;
using Microsoft.EntityFrameworkCore;


namespace Api.Data
{
    public class DataContext : DbContext // Usar, modificar o ampliar métodos de esta clase
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}  
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<RolPagina> RolPaginas {get;set;}
        public DbSet<Cliente> Cliente {get;set;}
        public DbSet<ValorTabla> ValorTabla {get;set;}
        public DbSet<Destinatario> Destinatarios {get;set;}


        public DbSet<Estado> Estados {get;set;}
        public DbSet<OrdenServicio> OrdenServicios {get;set;}
        public DbSet<Carga> Cargas {get;set;}
        public DbSet<GuiaRemisionRemitente> guiaRemisionRemitentes {get;set;}
    
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PaginaConfiguration());
            builder.ApplyConfiguration(new RolConfiguration ());
            builder.ApplyConfiguration(new RolPaginaConfiguration());

            builder.ApplyConfiguration(new ClienteConfiguration());
            builder.ApplyConfiguration(new EstadoConfiguration());
            builder.ApplyConfiguration(new DestinatarioConfiguration());
            
            builder.ApplyConfiguration(new ValorTablaConfiguration());
            builder.ApplyConfiguration(new OrdenServicioConfiguration());
            builder.ApplyConfiguration(new CargaConfiguration());
            builder.ApplyConfiguration(new GuiaRemisionRemitenteConfiguration());

            base.OnModelCreating(builder);

           

            builder.Entity<RolPagina>()
                .Property(x=>x.permisos).HasMaxLength(3).IsRequired();
            
            builder.Entity<RolPagina>()
                .ToTable("RolesPaginas","Seguridad")
                .HasKey(rp => new { rp.IdRol, rp.IdPagina });
                

            builder.Entity<RolPagina>()
                .HasOne(rp => rp.Pagina)
                .WithMany(p => p.RolPaginas)
                .HasForeignKey(p => p.IdPagina);
            builder.Entity<RolPagina>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPaginas)
                .HasForeignKey(r => r.IdRol);



            builder.Entity<RolUser>()
                .ToTable("RolesUsers","Seguridad")
                .HasKey(rp => new { rp.RolId, rp.UserId });
            builder.Entity<RolUser>()
                .HasOne(rp => rp.Rol)
                .WithMany(p => p.RolUser)
                .HasForeignKey(p => p.RolId);

          

            builder.Entity<RolUser>()
                .HasOne(rp => rp.User) 
                .WithMany(r => r.RolUser)
                .HasForeignKey(r => r.UserId);
        }
    }
}
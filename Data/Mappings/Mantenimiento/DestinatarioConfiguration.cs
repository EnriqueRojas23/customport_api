
using Api.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings.Mantenimiento
{
    public class DestinatarioConfiguration : IEntityTypeConfiguration<Destinatario>
    {
        public void Configure(EntityTypeBuilder<Destinatario> builder)
        {
            builder.ToTable("Destinatario","Mantenimiento");
            builder.HasKey(x=>x.id);
            builder.Property(x=>x.razonsocial).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.idcliente).IsRequired();
            
        }
    }
}
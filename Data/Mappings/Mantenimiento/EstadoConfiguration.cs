

using Api.Domain.Mantenimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings.Mantenimiento
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados","Mantenimiento");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Descripcion).HasMaxLength(100).IsRequired();
            
        }
    }
}
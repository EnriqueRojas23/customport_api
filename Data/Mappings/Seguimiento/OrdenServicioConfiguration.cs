using API.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings.Seguimiento
{
    public class OrdenServicioConfiguration  : IEntityTypeConfiguration<OrdenServicio>
    {
        public void Configure(EntityTypeBuilder<OrdenServicio> builder)
        {
            builder.ToTable("OrdenServicio","Seguimiento");
            builder.HasKey(x=>x.id);
        }
    }
}
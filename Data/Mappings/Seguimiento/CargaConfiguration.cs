using API.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings.Seguimiento
{
    public class CargaConfiguration  : IEntityTypeConfiguration<Carga>
    {
        public void Configure(EntityTypeBuilder<Carga> builder)
        {
            builder.ToTable("Carga","Seguimiento");
            builder.HasKey(x=>x.id);
        }
    }
}
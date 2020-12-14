using API.Domain.Seguimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings.Seguimiento
{
    public class GuiaRemisionRemitenteConfiguration  : IEntityTypeConfiguration<GuiaRemisionRemitente>
    {
        public void Configure(EntityTypeBuilder<GuiaRemisionRemitente> builder)
        {
            builder.ToTable("GuiaRemisionRemitente","Seguimiento");
            builder.HasKey(x=>x.id);
        }
    }
}
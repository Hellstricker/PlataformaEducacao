using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Data.Mappings
{
    public class AulaFinalizadaMapping : IEntityTypeConfiguration<AulaFinalizada>
    {
        public void Configure(EntityTypeBuilder<AulaFinalizada> builder)
        {
            builder.ToTable("AulasFinalizadas");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AulaId).IsRequired();
            builder.Property(a => a.MatriculaId).IsRequired();
            builder.HasOne(a => a.Matricula)
                .WithMany()
                .HasForeignKey(a => a.MatriculaId);
        }
    }
}

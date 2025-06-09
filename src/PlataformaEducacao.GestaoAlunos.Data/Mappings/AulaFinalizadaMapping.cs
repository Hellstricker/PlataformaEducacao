using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.GestaoAlunos.Domain;

namespace PlataformaEducacao.GestaoAlunos.Data.Mappings
{
    public class AulaFinalizadaMapping : IEntityTypeConfiguration<AulaFinalizada>
    {
        public void Configure(EntityTypeBuilder<AulaFinalizada> builder)
        {
            builder.ToTable("AulasFinalizadas");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AulaId).IsRequired();
            builder.HasOne(a => a.Matricula)
                .WithMany(m => m.AulasFinalizadas)
                .HasForeignKey(a => a.MatriculaId);
        }
    }
}

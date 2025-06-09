using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.GestaoCursos.Domain;

namespace PlataformaEducacao.GestaoCursos.Data.Mappings
{
    public class AulaMapping : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.ToTable("Aulas");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Titulo).IsRequired().HasColumnType("varchar(150)");
            builder.Property(a => a.Conteudo).IsRequired().HasColumnType("varchar(500)");
            builder.Property(a => a.TotalMinutos).IsRequired();
            builder.HasOne(a => a.Curso).WithMany(c => c.Aulas).HasForeignKey(a => a.CursoId);
        }
    }
}

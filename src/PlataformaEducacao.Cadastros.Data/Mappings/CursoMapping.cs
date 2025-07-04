using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Cadastros.Domain;

namespace PlataformaEducacao.Cadastros.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType($"varchar({Curso.MAXIMO_CARACTERES_TITULO})");
            builder.Property(c => c.Valor)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(cp => cp.ConteudoProgramatico, cp =>
            {
                cp.Property(cp => cp.Descricao).HasColumnName("Descricao").HasColumnType($"varchar({ConteudoProgramatico.MAXIMO_CARACTERES_DESCRICAO})").IsRequired();
                cp.Property(cp => cp.CargaHoraria).HasColumnName("CargaHoraria").IsRequired();
            });
        }
    }
}

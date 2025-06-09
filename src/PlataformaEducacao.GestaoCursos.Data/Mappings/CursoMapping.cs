using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.GestaoCursos.Domain;

namespace PlataformaEducacao.GestaoCursos.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(c => c.Valor)                
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(cp => cp.ConteudoProgramatico, cp => {
                cp.Property(cp => cp.Objetivo).HasColumnName("Objetivo").HasColumnType("varchar(500)").IsRequired();
                cp.Property(cp => cp.Conteudo).HasColumnName("Conteudo").HasColumnType("varchar(500)").IsRequired();
            });
        }
    }
}

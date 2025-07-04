using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Cadastros.Domain;

namespace PlataformaEducacao.Cadastros.Data.Mappings
{
    public partial class AulaMapping : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.ToTable("Aulas");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Titulo).IsRequired().HasColumnType($"varchar({Aula.MAXIMO_CARACTERES_TITULO})");
            builder.Property(a => a.Conteudo).IsRequired().HasColumnType($"varchar({Aula.MAXIMO_CARACTERES_CONTEUDO})");
            builder.Property(a => a.Duracao).IsRequired();
            builder.HasOne(a => a.Curso).WithMany(c => c.Aulas).HasForeignKey(a => a.CursoId);
        }
    }
}

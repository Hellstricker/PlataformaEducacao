using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Data.Mappings
{
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).IsRequired().HasColumnType($"varchar({Aluno.NOME_MAX_CARACTERES})");
            //builder.HasMany(a => a.Matriculas)
            //    .WithOne(m => m.Aluno)
            //    .HasForeignKey(m => m.AlunoId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Data.Mappings
{
    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");
            builder.HasKey(m => m.Id);
            builder.OwnsOne(m => m.Curso, c =>  {
                c.Property(p => p.CursoId).HasColumnName("CursoId").HasColumnType($"varchar(50)").IsRequired();
                c.Property(p => p.CursoNome).HasColumnName("CursoNome").HasColumnType($"varchar({Matricula.MAX_NOMECURSO_CHAR})").IsRequired();
                c.Property(p => p.CursoValor).HasColumnName("CursoValor").HasColumnType("decimal(18,2)").IsRequired();
                c.Property(p => p.CursoTotalAulas).HasColumnName("CursoTotalAulas").IsRequired();
                c.OwnsOne(m => m.HistoricoAprendizado, historico =>
                {
                    historico.Property(p => p.Progresso).HasColumnName("CursoProgresso").IsRequired(false).HasColumnType("decimal(5,2)");
                    historico.Ignore(h => h.AulasFinalizadas);
                });                
            });
            builder.Property(m => m.Status)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasConversion(statusMatricula => statusMatricula.ToString(), statusMatricula => (StatusMatriculaEnum)Enum.Parse(typeof(StatusMatriculaEnum), statusMatricula));
            builder.HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);
        }
    }
}

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
            builder.OwnsOne(m => m.Curso, c =>            {
                c.Property(p => p.CursoId).HasColumnType($"varchar(50)").IsRequired();
                c.Property(p => p.CursoNome).HasColumnType($"varchar({Matricula.MAX_NOMECURSO_CHAR})").IsRequired();
                c.Property(p => p.CursoValor).HasColumnType("decimal(18,2)").IsRequired();
                c.Property(p => p.CursoTotalAulas).IsRequired();
            });            
            builder.Property(m => m.Status)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasConversion(statusMatricula => statusMatricula.ToString(), statusMatricula => (StatusMatriculaEnum)Enum.Parse(typeof(StatusMatriculaEnum), statusMatricula));
            builder.HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);
            //builder.HasMany(m => m.AulasFinalizadas)
            //    .WithOne(a => a.Matricula)
            //    .HasForeignKey(a => a.MatriculaId);
            //builder.OwnsOne(m => m.HistoricoAprendizado)
            //    .Property(h => h.Progresso)
            //    .IsRequired(false)
            //    .HasColumnType("decimal(5,2)");
            //builder.HasOne(m => m.Certificado)
            //    .WithOne(c => c.Matricula);

        }
    }
}

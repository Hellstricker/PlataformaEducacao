using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.GestaoAlunos.Domain;

namespace PlataformaEducacao.GestaoAlunos.Data.Mappings
{
    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("Matriculas");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CursoId).IsRequired();
            builder.Property(m => m.NomeCurso)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);
            builder.Property(m => m.StatusMatricula)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasConversion(statusMatricula => statusMatricula.ToString(), statusMatricula=> (StatusMatricula)Enum.Parse(typeof(StatusMatricula), statusMatricula));
            builder.HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);
            builder.HasMany(m => m.AulasFinalizadas)
                .WithOne(a => a.Matricula)
                .HasForeignKey(a => a.MatriculaId);
            builder.OwnsOne(m => m.HistoricoAprendizado)
                .Property(h => h.Progresso)
                .IsRequired(false)
                .HasColumnType("decimal(5,2)");
            builder.HasOne(m => m.Certificado)
                .WithOne(c => c.Matricula);
                
        }
    }
}

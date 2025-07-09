using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Data.Mappings
{
    public class CertificadoMapping : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificados");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.NumeroCertificado)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.NomeCurso)
                .IsRequired()
                .HasMaxLength(100);            
            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasColumnType("bit");
            builder.HasOne(c => c.Aluno)
                .WithMany(a => a.Certificados);                
        }
    }
}

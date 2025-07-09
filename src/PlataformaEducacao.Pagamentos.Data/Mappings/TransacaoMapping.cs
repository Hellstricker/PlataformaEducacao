using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Pagamentos.Business;

namespace PlataformaEducacao.Pagamentos.Data.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PagamentoId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.MatriculaId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");
            builder.Property(t => t.Total)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.StatusTransacao)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusTransacao)Enum.Parse(typeof(StatusTransacao), v))
                .HasColumnType("varchar(20)");
        }
    }
}

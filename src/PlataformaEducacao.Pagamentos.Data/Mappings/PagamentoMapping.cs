using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacao.Pagamentos.Business;

namespace PlataformaEducacao.Pagamentos.Data.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamentos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.AlunoId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");
            builder.Property(p => p.CursoId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");
            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(p => p.DadosCartao, dc =>
            {
                dc.Property(d => d.NomeCartao).IsRequired().HasColumnType("varchar(100)");
                dc.Property(d => d.NumeroCartao).IsRequired().HasColumnType("varchar(20)");
                dc.Property(d => d.MesAnoExpiracao).IsRequired().HasColumnType("varchar(7)");
                dc.Property(d => d.Ccv).IsRequired().HasColumnType("varchar(4)");
            });

            builder.HasOne(p => p.Transacao)
                .WithOne(t => t.Pagamento);                
        }
    }
}

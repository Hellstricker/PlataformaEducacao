﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlataformaEducacao.Pagamentos.Data;

#nullable disable

namespace PlataformaEducacao.Pagamentos.Data.Migrations
{
    [DbContext(typeof(PagamentosContext))]
    partial class PagamentosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.17");

            modelBuilder.Entity("PlataformaEducacao.Pagamentos.Business.Pagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MatriculaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Pagamentos", (string)null);
                });

            modelBuilder.Entity("PlataformaEducacao.Pagamentos.Business.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MatriculaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PagamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StatusTransacao")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PagamentoId")
                        .IsUnique();

                    b.ToTable("Transacoes", (string)null);
                });

            modelBuilder.Entity("PlataformaEducacao.Pagamentos.Business.Pagamento", b =>
                {
                    b.OwnsOne("PlataformaEducacao.Pagamentos.Business.DadosCartao", "DadosCartao", b1 =>
                        {
                            b1.Property<Guid>("PagamentoId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Ccv")
                                .IsRequired()
                                .HasColumnType("varchar(4)");

                            b1.Property<string>("MesAnoExpiracao")
                                .IsRequired()
                                .HasColumnType("varchar(7)");

                            b1.Property<string>("NomeCartao")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("NumeroCartao")
                                .IsRequired()
                                .HasColumnType("varchar(20)");

                            b1.HasKey("PagamentoId");

                            b1.ToTable("Pagamentos");

                            b1.WithOwner()
                                .HasForeignKey("PagamentoId");
                        });

                    b.Navigation("DadosCartao")
                        .IsRequired();
                });

            modelBuilder.Entity("PlataformaEducacao.Pagamentos.Business.Transacao", b =>
                {
                    b.HasOne("PlataformaEducacao.Pagamentos.Business.Pagamento", "Pagamento")
                        .WithOne("Transacao")
                        .HasForeignKey("PlataformaEducacao.Pagamentos.Business.Transacao", "PagamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pagamento");
                });

            modelBuilder.Entity("PlataformaEducacao.Pagamentos.Business.Pagamento", b =>
                {
                    b.Navigation("Transacao")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

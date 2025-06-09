using AutoMapper;
using PlataformaEducacao.GestaoCursos.Application.Dtos;
using PlataformaEducacao.GestaoCursos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacao.GestaoCursos.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Curso, CursoDto>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => GetConteudo(src.ConteudoProgramatico)))
                .ForMember(dest => dest.Objetivo, opt => opt.MapFrom(src => GetObjetivo(src.ConteudoProgramatico)));
            CreateMap<Aula, AulaDto>();
        }

        private string? GetConteudo(ConteudoProgramatico? conteudoProgramatico) => conteudoProgramatico?.Conteudo ?? null;
        private string? GetObjetivo(ConteudoProgramatico? conteudoProgramatico) => conteudoProgramatico?.Objetivo ?? null;
    }
}

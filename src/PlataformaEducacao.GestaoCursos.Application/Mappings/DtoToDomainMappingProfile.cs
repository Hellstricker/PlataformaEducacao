using AutoMapper;
using PlataformaEducacao.GestaoCursos.Application.Dtos;
using PlataformaEducacao.GestaoCursos.Domain;

namespace PlataformaEducacao.GestaoCursos.Application.Mappings
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<CadastrarCursoDto, Curso>()
                .ConstructUsing(c => new Curso(c.Nome, c.Valor, new ConteudoProgramatico(c.Objetivo, c.Conteudo)));

            CreateMap<CadastrarAulaDto, Aula>();
        }
    }
}

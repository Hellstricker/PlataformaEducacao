using AutoMapper;
using PlataformaEducacao.GestaoAlunos.Application.Dtos;
using PlataformaEducacao.GestaoAlunos.Domain;

namespace PlataformaEducacao.GestaoAlunos.Application.Mappings
{
    public class DtoToDomainMappingProfile:Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<MatricularAlunoDto, Matricula>();
        }
    }
}

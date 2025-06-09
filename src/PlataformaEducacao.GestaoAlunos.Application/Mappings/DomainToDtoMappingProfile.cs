using AutoMapper;
using PlataformaEducacao.GestaoAlunos.Application.Dtos;
using PlataformaEducacao.GestaoAlunos.Domain;

namespace PlataformaEducacao.GestaoAlunos.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Matricula, MatriculaParaPagamentoDto>()                               
                .ForMember(dest => dest.StatusMatricula, opt => opt.MapFrom(src => src.StatusMatricula.ToString()));
            CreateMap<Aluno, AlunoCompletoDto>();
            CreateMap<Matricula, MatriculaDto>()
                .ForMember(dest => dest.StatusMatricula, opt => opt.MapFrom(src => src.StatusMatricula.ToString()))
                .ForMember(dest => dest.Progresso, opt => opt.MapFrom(src => src.HistoricoAprendizado.Progresso));
            CreateMap<AulaFinalizada, AulaFinalizadaDto>()
                .ForMember(dest=>dest.DataFinalizacao, opt=> opt.MapFrom(src=>src.DataCadastro));
            CreateMap<Certificado, CertificadoDto>()
                .ForMember(dest => dest.DataConclusao, opt => opt.MapFrom(src => src.DataCadastro));
                
        }
    }
}

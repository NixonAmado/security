using Api.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Api.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CategoriaPer, CategoriaPerDto>().ReverseMap();
        CreateMap<Ciudad, CiudadDto>().ReverseMap();
        CreateMap<ContactoPer, ContactoPerDto>().ReverseMap();
        CreateMap<Contrato, ContratoDto>().ReverseMap();
        CreateMap<Departamento, DepartamentoDto>().ReverseMap();
        CreateMap<DirPersona, DirPersonaDto>().ReverseMap();
        CreateMap<Estado, EstadoDto>().ReverseMap();
        CreateMap<Pais, PaisDto>().ReverseMap();
        CreateMap<Persona, PersonaDto>().ReverseMap();
        CreateMap<Programacion, ProgramacionDto>().ReverseMap();
        CreateMap<TipoContacto, TipoContactoDto>().ReverseMap();
        CreateMap<TipoDireccion, TipoDireccionDto>().ReverseMap();
        CreateMap<TipoPersona, TipoPersonaDto>().ReverseMap();
        CreateMap<Turno, TurnoDto>().ReverseMap();
    }
}

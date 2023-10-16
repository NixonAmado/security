namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRol Roles { get; }
    IUser Users { get; }
    ICategoriaPer CategoriaPers { get; }
    ICiudad Ciudades { get; }
    IContactoPer ContactoPers { get; }
    IContrato Contratos { get; }
    IDepartamento Departamentos { get; }
    IDirPersona DirPersonas { get; }
    IEstado Estados { get; }
    IPais Paises { get; }
    IPersona Personas { get; }
    IProgramacion Programaciones { get; }
    ITipoContacto TipoContactos { get; }
    ITipoDireccion TipoDirecciones { get; }
    ITipoPersona TipoPersonas { get; }
    ITurno Turnos { get; }
    Task<int> SaveAsync();
}

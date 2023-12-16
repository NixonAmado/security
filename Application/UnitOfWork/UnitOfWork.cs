using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SecurityContext _context;
    private CategoriaPerRepository _categoriaPerRepository;
    private ContactoPerRepository _contactoPerRepository;
    private CiudadRepository _ciudadRepository;
    private ContratoRepository _contratoRepository;
    private DepartamentoRepository _departamentoRepository;
    private DirPersonaRepository dirPersonaRepository;
    private EstadoRepository _estadoRepository;
    private PaisRepository _paisRepository;
    private PersonaRepository _personaRepository;
    private ProgramacionRepository _programacionRepository;
    private TipoContactoRepository _tipoContactoRepository;
    private TipoDireccionRepository _tipoDireccionRepository;
    private TipoPersonaRepository _tipoPersonaRepository;
    private TurnoRepository _turnoRepository;

    public UnitOfWork(SecurityContext context)
    {
        _context = context;
    }

    public ICategoriaPer CategoriaPers
    {
        get
        {
            if (_categoriaPerRepository == null)
            {
                _categoriaPerRepository = new CategoriaPerRepository(_context);
            }
            return _categoriaPerRepository;
        }
    }

    public IContactoPer ContactoPers
    {
        get
        {
            if (_contactoPerRepository == null)
            {
                _contactoPerRepository = new ContactoPerRepository(_context);
            }
            return _contactoPerRepository;
        }
    }

    public ICiudad Ciudades
    {
        get
        {
            if (_ciudadRepository == null)
            {
                _ciudadRepository = new CiudadRepository(_context);
            }
            return _ciudadRepository;
        }
    }

    public IContrato Contratos
    {
        get
        {
            if (_contratoRepository == null)
            {
                _contratoRepository = new ContratoRepository(_context);
            }
            return _contratoRepository;
        }
    }

    public IDepartamento Departamentos
    {
        get
        {
            if (_departamentoRepository == null)
            {
                _departamentoRepository = new DepartamentoRepository(_context);
            }
            return _departamentoRepository;
        }
    }

    public IDirPersona DirPersonas
    {
        get
        {
            if (dirPersonaRepository == null)
            {
                dirPersonaRepository = new DirPersonaRepository(_context);
            }
            return dirPersonaRepository;
        }
    }

    public IEstado Estados
    {
        get
        {
            if (_estadoRepository == null)
            {
                _estadoRepository = new EstadoRepository(_context);
            }
            return _estadoRepository;
        }
    }

    public IPais Paises
    {
        get
        {
            if (_paisRepository == null)
            {
                _paisRepository = new PaisRepository(_context);
            }
            return _paisRepository;
        }
    }

    public IPersona Personas
    {
        get
        {
            if (_personaRepository == null)
            {
                _personaRepository = new PersonaRepository(_context);
            }
            return _personaRepository;
        }
    }

    public IProgramacion Programaciones
    {
        get
        {
            if (_programacionRepository == null)
            {
                _programacionRepository = new ProgramacionRepository(_context);
            }
            return _programacionRepository;
        }
    }

    public ITipoContacto TipoContactos
    {
        get
        {
            if (_tipoContactoRepository == null)
            {
                _tipoContactoRepository = new TipoContactoRepository(_context);
            }
            return _tipoContactoRepository;
        }
    }

    public ITipoDireccion TipoDirecciones
    {
        get
        {
            if (_tipoDireccionRepository == null)
            {
                _tipoDireccionRepository = new TipoDireccionRepository(_context);
            }
            return _tipoDireccionRepository;
        }
    }

    public ITipoPersona TipoPersonas
    {
        get
        {
            if (_tipoPersonaRepository == null)
            {
                _tipoPersonaRepository = new TipoPersonaRepository(_context);
            }
            return _tipoPersonaRepository;
        }
    }

    public ITurno Turnos
    {
        get
        {
            if (_turnoRepository == null)
            {
                _turnoRepository = new TurnoRepository(_context);
            }
            return _turnoRepository;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

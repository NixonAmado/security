using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class TipoPersonaRepository : GenericRepository<TipoPersona>, ITipoPersona
{
    private readonly SecurityContext _context;

    public TipoPersonaRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

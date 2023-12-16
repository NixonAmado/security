using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class TurnoRepository : GenericRepository<Turno>, ITurno
{
    private readonly SecurityContext _context;

    public TurnoRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

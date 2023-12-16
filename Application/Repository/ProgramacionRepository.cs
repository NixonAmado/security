using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class ProgramacionRepository : GenericRepository<Programacion>, IProgramacion
{
    private readonly SecurityContext _context;

    public ProgramacionRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

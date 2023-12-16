using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class DirPersonaRepository : GenericRepository<DirPersona>, IDirPersona
{
    private readonly SecurityContext _context;

    public DirPersonaRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly SecurityContext _context;

    public RolRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

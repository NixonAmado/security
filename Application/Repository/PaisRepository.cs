using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class PaisRepository : GenericRepository<Pais>, IPais
{
    private readonly SecurityContext _context;

    public PaisRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

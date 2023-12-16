using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class CiudadRepository : GenericRepository<Ciudad>, ICiudad
{
    private readonly SecurityContext _context;

    public CiudadRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

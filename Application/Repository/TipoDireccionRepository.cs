using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class TipoDireccionRepository : GenericRepository<TipoDireccion>, ITipoDireccion
{
    private readonly SecurityContext _context;

    public TipoDireccionRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

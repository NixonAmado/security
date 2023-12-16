using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class TipoContactoRepository : GenericRepository<TipoContacto>, ITipoContacto
{
    private readonly SecurityContext _context;

    public TipoContactoRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

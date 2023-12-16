using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class CategoriaPerRepository : GenericRepository<CategoriaPer>, ICategoriaPer
{
    private readonly SecurityContext _context;

    public CategoriaPerRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

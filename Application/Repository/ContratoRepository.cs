using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class ContratoRepository : GenericRepository<Contrato>, IContrato
{
    private readonly SecurityContext _context;

    public ContratoRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

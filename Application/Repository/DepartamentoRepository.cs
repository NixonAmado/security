using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
{
    private readonly SecurityContext _context;

    public DepartamentoRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

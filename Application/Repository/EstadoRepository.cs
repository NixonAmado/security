using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Application.Repository;

public class EstadoRepository : GenericRepository<Estado>, IEstado
{
    private readonly SecurityContext _context;

    public EstadoRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

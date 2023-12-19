using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public override async Task<IEnumerable<Pais>> GetAllAsync()
    {
        return await _context.Paises.ToListAsync();
    }

    public override async Task<Pais> GetByIdAsync(int id)
    {
        return await _context.Paises.FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<(int totalRecords, IEnumerable<Pais> records)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Paises as IQueryable<Pais>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return (totalRecords, records);
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly SecurityContext _context;

    public PersonaRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

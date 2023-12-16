using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class ContactoPerRepository : GenericRepository<ContactoPer>, IContactoPer
{
    private readonly SecurityContext _context;

    public ContactoPerRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}

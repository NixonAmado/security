using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public partial class SecurityContext : DbContext
{
    public SecurityContext() { }

    public SecurityContext(DbContextOptions<SecurityContext> options)
        : base(options) { }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<CategoriaPer> CategoriaPers { get; set; }

    public virtual DbSet<Ciudad> Ciudades { get; set; }

    public virtual DbSet<ContactoPer> ContactoPers { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DirPersona> DirPersonas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Pais> Paises { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Programacion> Programacions { get; set; }

    public virtual DbSet<TipoContacto> TipoContactos { get; set; }

    public virtual DbSet<TipoDireccion> TipoDireccions { get; set; }

    public virtual DbSet<TipoPersona> TipoPersonas { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");
    }
}

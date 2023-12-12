using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Persona
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int IdTipoPersona { get; set; }

    public int IdCategoria { get; set; }

    public int IdCiudad { get; set; }

    public virtual ICollection<ContactoPer> ContactoPers { get; set; } = new List<ContactoPer>();

    public virtual ICollection<Contrato> ContratoIdClienteNavigations { get; set; } =
        new List<Contrato>();

    public virtual ICollection<Contrato> ContratoIdEmpleadoNavigations { get; set; } =
        new List<Contrato>();

    public virtual ICollection<DirPersona> DirPersonas { get; set; } = new List<DirPersona>();

    public virtual CategoriaPer IdCategoriaNavigation { get; set; } = null!;

    public virtual Ciudad IdCiudadNavigation { get; set; } = null!;

    public virtual TipoPersona IdTipoPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Programacion> Programacions { get; set; } = new List<Programacion>();
}

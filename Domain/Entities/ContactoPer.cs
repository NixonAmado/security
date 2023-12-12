using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ContactoPer
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdTipoContacto { get; set; }

    public int IdPersona { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual TipoContacto IdTipoContactoNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DirPersona
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdTipoDireccion { get; set; }

    public int IdPersona { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual TipoDireccion IdTipoDireccionNavigation { get; set; } = null!;
}

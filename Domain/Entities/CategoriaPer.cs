using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class CategoriaPer
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}

using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoDireccion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DirPersona> DirPersonas { get; set; } = new List<DirPersona>();
}

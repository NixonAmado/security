using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoContacto
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ContactoPer> ContactoPers { get; set; } = new List<ContactoPer>();
}

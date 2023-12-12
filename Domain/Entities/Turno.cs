using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Turno
{
    public int Id { get; set; }

    public string NombreTurno { get; set; } = null!;

    public TimeOnly HoraTurnoInicio { get; set; }

    public TimeOnly HoraTurnoFin { get; set; }

    public virtual ICollection<Programacion> Programacions { get; set; } = new List<Programacion>();
}

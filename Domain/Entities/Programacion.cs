using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Programacion
{
    public int Id { get; set; }

    public int IdContrato { get; set; }

    public int IdTurno { get; set; }

    public int IdEmpleado { get; set; }

    public virtual Contrato IdContratoNavigation { get; set; } = null!;

    public virtual Persona IdEmpleadoNavigation { get; set; } = null!;

    public virtual Turno IdTurnoNavigation { get; set; } = null!;
}

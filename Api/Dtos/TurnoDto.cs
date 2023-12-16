namespace Api.Dtos;

public class TurnoDto
{
    public int Id { get; set; }

    public string NombreTurno { get; set; } = null!;

    public TimeOnly HoraTurnoInicio { get; set; }

    public TimeOnly HoraTurnoFin { get; set; }
}

namespace Api.Dtos;

public class PersonaDto
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int IdTipoPersona { get; set; }

    public int IdCategoria { get; set; }

    public int IdCiudad { get; set; }
}

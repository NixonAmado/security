namespace Api.Dtos;

public class DirPersonaDto
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdTipoDireccion { get; set; }

    public int IdPersona { get; set; }
}

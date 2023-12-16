namespace Api.Dtos;

public class ContactoPerDto
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdTipoContacto { get; set; }

    public int IdPersona { get; set; }
}

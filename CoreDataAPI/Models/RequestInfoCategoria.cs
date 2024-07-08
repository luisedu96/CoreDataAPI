namespace CoreDataAPI.Models;

public partial class RequestInfoCategoria
{
    public int SkInfoCategoriaDetalle { get; set; }

    public string StrNombreCategoria { get; set; } = null!;

    public string StrNombreDetalle { get; set; } = null!;

    public string? StrObservacion { get; set; }

    public string? StrUriImagen { get; set; }

    public int SkInfoElemento { get; set; }

    public virtual RequestInfoElemento SkInfoElementoNavigation { get; set; } = null!;
}
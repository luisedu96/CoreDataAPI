namespace CoreDataAPI.Models;

public partial class RequestInfoElemento
{
    public int SkInfoElemento { get; set; }

    public string StrNombreElemento { get; set; } = null!;

    public int SkInfoUbicacion { get; set; }

    public virtual RequestInfoUbicacion SkInfoUbicacionNavigation { get; set; } = null!;

    public virtual ICollection<RequestInfoCategoria> ResponseInfoCategoriaDetalles { get; set; } = new List<RequestInfoCategoria>();
}

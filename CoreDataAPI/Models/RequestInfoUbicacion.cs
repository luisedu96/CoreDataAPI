namespace CoreDataAPI.Models;

public partial class RequestInfoUbicacion
{
    public int SkInfoUbicacion { get; set; }

    public string StrNombreUbicacion { get; set; } = null!;

    public int SkInfoUnidad { get; set; }

    public virtual RequestInfoUnidad SkInfoUnidadNavigation { get; set; } = null!;

    public virtual ICollection<RequestInfoElemento> ResponseInfoElementos { get; set; } = new List<RequestInfoElemento>();
}

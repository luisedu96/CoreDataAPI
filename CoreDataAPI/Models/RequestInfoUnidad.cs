namespace CoreDataAPI.Models;

public partial class RequestInfoUnidad
{
    public int SkInfoUnidad { get; set; }

    public string StrNombreUser { get; set; } = null!;

    public string StrEmail { get; set; } = null!;

    public string StrTipoEntrega { get; set; } = null!;

    public string StrTipoProyecto { get; set; } = null!;

    public string StrProyecto { get; set; } = null!;

    public string StrUnidad { get; set; } = null!;

    public DateOnly DateFechaProgramada { get; set; }

    public DateTime DateFechaEntrega { get; set; }

    public virtual ICollection<RequestInfoUbicacion> ResponseInfoUbicaciones { get; set; } = new List<RequestInfoUbicacion>();
}

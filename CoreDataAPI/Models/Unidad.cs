namespace CoreDataAPI.Models;

public partial class Unidad
{
    public int SkUnidad { get; set; }

    public string? NkUnidad { get; set; }

    public string? StrCodUnidad { get; set; }

    public string? NumCodTipoInmueble { get; set; }

    public string? StrTipoInmueble { get; set; }

    public string? StrTipoUnidad { get; set; }

    public string? StrAptUnidad { get; set; }

    public string? StrEstadoUnidad { get; set; }

    public string? StrTipologia { get; set; }

    public string? StrTipolArea { get; set; }

    public int? NumAreaConstruida { get; set; }

    public double? ValUnidad { get; set; }

    public int? ValUnidadListaVigente { get; set; }

    public DateOnly FecCargaDwh { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}

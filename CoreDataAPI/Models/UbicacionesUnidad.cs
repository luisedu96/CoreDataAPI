namespace CoreDataAPI.Models;

public partial class UbicacionesUnidad
{
    public int SkUbicacion { get; set; }

    public string StrNombreUbicacion { get; set; } = null!;

    public string StrTipoProyecto { get; set; } = null!;

    public virtual ICollection<RelacionTipologiaUbicacion> RelacionTipologiaUbicacions { get; set; } = new List<RelacionTipologiaUbicacion>();

    public virtual ICollection<RelacionUbicacionElemento> RelacionUbicacionElementos { get; set; } = new List<RelacionUbicacionElemento>();
}

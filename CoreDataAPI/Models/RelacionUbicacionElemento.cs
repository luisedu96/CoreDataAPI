namespace CoreDataAPI.Models;

public partial class RelacionUbicacionElemento
{
    public int SkRelation { get; set; }

    public int SkUbicacionesUnidad { get; set; }

    public string StrTipoEntrega { get; set; } = null!;

    public string StrTipoProyecto { get; set; } = null!;

    public int SkElemento { get; set; }

    public virtual Elemento SkElementoNavigation { get; set; } = null!;

    public virtual UbicacionesUnidad SkUbicacionesUnidadNavigation { get; set; } = null!;
}

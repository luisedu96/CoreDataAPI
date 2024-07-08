namespace CoreDataAPI.Models;

public partial class RelacionTipologiaUbicacion
{
    public int SkRelation { get; set; }

    public string StrTipologia { get; set; } = null!;

    public int SkUbicacion { get; set; }

    public virtual UbicacionesUnidad SkUbicacionNavigation { get; set; } = null!;
}

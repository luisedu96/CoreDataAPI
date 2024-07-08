namespace CoreDataAPI.Models;

public partial class Inventario
{
    public int SkInventario { get; set; }

    public int? SkEmpresa { get; set; }

    public int? SkMacroproyecto { get; set; }

    public int? SkProyecto { get; set; }

    public int? SkUnidad { get; set; }

    public int? SkCliente { get; set; }

    public int? SkVenta { get; set; }

    public string? SkProyeccion { get; set; }

    public int? SkTiempo { get; set; }

    public string SkStatusInventario { get; set; } = null!;

    public string? NumCodVenta { get; set; }

    public float? FltAreaConstruida { get; set; }

    public float? FltAreaPrivada { get; set; }

    public float? FltAreaTerraza { get; set; }

    public float? FltAreaBalcon { get; set; }

    public float? FltAreaPatio { get; set; }

    public float? FltAreaTotal { get; set; }

    public float? FltAreaTecnica { get; set; }

    public float? FltAreaJardineria { get; set; }

    public int? NumPiso { get; set; }

    public int? NumAlcobas { get; set; }

    public string? NumKeyOrder { get; set; }

    public int? NumPromedioVent { get; set; }

    public int? NumTotalProyeccion { get; set; }

    public int? NumMetaUnidades { get; set; }

    public int? NumMetaDinero { get; set; }

    public int? ValUnidad { get; set; }

    public DateOnly FecCargaDwh { get; set; }

    public virtual Proyecto? SkProyectoNavigation { get; set; }

    public virtual Unidad? SkUnidadNavigation { get; set; }
}

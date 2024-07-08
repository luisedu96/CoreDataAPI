namespace CoreDataAPI.Models;

public partial class Proyecto
{
    public int SkProyecto { get; set; }

    public string? NkProyecto { get; set; }

    public string? NumIdProyecto { get; set; }

    public string? StrNombreProyecto { get; set; }

    public string? NumIdClaseProyecto { get; set; }

    public string? StrNombreClase { get; set; }

    public string? NumEstado { get; set; }

    public string? StrNombreEstado { get; set; }

    public string? NumNaturaleza { get; set; }

    public string? StrCiudad { get; set; }

    public string? StrEtapa { get; set; }

    public string? StrEstrato { get; set; }

    public string? StrInterior { get; set; }

    public string? StrEntidadCredito { get; set; }

    public string? StrNitEntCredito { get; set; }

    public string? StrEntFiduciaria { get; set; }

    public string? StrNitEntFiduciaria { get; set; }

    public string? BanVis { get; set; }

    public string StrTipoProyecto { get; set; } = null!;

    public string? StrDireccion { get; set; }

    public string? StrEtapaProyecto { get; set; }

    public string? StrFiducia { get; set; }

    public string? FecEntrega { get; set; }

    public string? FecFinalizacion { get; set; }

    public DateOnly FecCargaDwh { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}

namespace CoreDataAPI.Models;

public partial class Detalle
{
    public int SkDetalle { get; set; }

    public string StrNombreDetalle { get; set; } = null!;

    public int SkCategoria { get; set; }

    public virtual Categoria SkCategoriaNavigation { get; set; } = null!;
}

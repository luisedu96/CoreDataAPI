namespace CoreDataAPI.Models;
public partial class Categoria
{
    public int SkCategoria { get; set; }

    public string StrNombreCategoria { get; set; } = null!;

    public virtual ICollection<Detalle> TblDetalles { get; set; } = new List<Detalle>();
}

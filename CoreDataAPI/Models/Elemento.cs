namespace CoreDataAPI.Models;

public partial class Elemento
{
    public int SkElemento { get; set; }

    public string StrNombreElemento { get; set; } = null!;

    public virtual ICollection<RelacionUbicacionElemento> RelacionUbicacionElementos { get; set; } = new List<RelacionUbicacionElemento>();
}

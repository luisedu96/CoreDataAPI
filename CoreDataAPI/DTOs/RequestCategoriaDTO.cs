namespace CoreDataAPI.DTOs
{
    public class RequestCategoriaDTO
    {
        public int SkElemento { get; set; }
        public string Categoria { get; set; } = null!;
        public string Detalle { get; set; } = null!;
        public string? Observacion { get; set; }
        public string? ImageBase64 { get; set; }
    }
}

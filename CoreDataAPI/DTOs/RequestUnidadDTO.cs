namespace CoreDataAPI.DTOs
{
    public class RequestUnidadDTO
    {
        public string ResponsableName { get; set; } = null!;

        public string ResponsableEmail { get; set; } = null!;

        public string TipoEntrega { get; set; } = null!;

        public string TipoProyecto { get; set; } = null!;

        public string Proyecto { get; set; } = null!;

        public string Apartamento { get; set; } = null!;

        public string? Tipologia { get; set; }

        public DateOnly FechaProgramada { get; set; }

        public string FechaEntrega { get; set; } = null!;
    }
}

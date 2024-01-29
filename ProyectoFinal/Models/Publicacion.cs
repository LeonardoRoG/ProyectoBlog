namespace ProyectoFinal.Models
{
    public class Publicacion
    {
        public string Titulo { get; set; } = string.Empty; // Por defecto el valor es vacío, para quitar la advertencia de posible null
        public string SubTitulo { get; set; } = string.Empty;
        public string Creador { get; set; } = string.Empty;
        public string Cuerpo { get; set; } = string.Empty;
        public DateTime Creacion { get; set; } = DateTime.Now;
        public string Imagen { get; set; } = string.Empty;

    }
}

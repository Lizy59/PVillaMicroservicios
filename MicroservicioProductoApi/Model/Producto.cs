using System.ComponentModel.DataAnnotations;

namespace MicroservicioProductoApi.Model
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required]
        public string NombreProducto { get; set; }

        [Range(1, 10000)]
        public double Precio { get; set; }
        public string Categoria { get; set; }   
        public string UrlImagen { get; set; }   
    }
}

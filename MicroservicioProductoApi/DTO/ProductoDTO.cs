namespace MicroservicioProductoApi.DTO
{
    public class ProductoDTO
    {
    
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public double Precio { get; set; }
        public string Categoria { get; set; }
        public string UrlImagen { get; set; }

        public List <object> Object { get; set; }

    }
}

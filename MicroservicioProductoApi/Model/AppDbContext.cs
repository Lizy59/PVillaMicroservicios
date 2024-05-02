using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MicroservicioProductoApi.Model
{
    public class AppDbContext: DbContext
    {
        //Se agrega contructor vacio
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public virtual DbSet<Producto> Producto { get; set; }
    }
}

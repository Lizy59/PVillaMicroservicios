using MicroservicioProductoApi.DTO;
using MicroservicioProductoApi.Model;
using MicroserviciosProductoApi.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioProductoApi.Controllers
{
    public class ProductoController : Controller
    {
        // GET: ProductoController

        private readonly AppDbContext _dbcontext;
        public ProductoController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {

            ResultDTO resultado = new ResultDTO();
            try
            {
                var query = _dbcontext.Producto.ToList();
                if(query != null)
                {
                    resultado.Objects = new List<object>();
                    foreach(var item in query)
                    {
                        ProductoDTO producto = new ProductoDTO();
                        producto.IdProducto = item.IdProducto;
                        producto.NombreProducto = item.NombreProducto;
                        producto.Precio = item.Precio;
                        producto.Categoria = item.Categoria;
                        producto.UrlImagen = item.UrlImagen;
                        resultado.Objects.Add(producto);
                    }
                    return Ok(resultado);
                }
                else
                {
                    resultado.ErrorMessage = "No hay registros";
                    resultado.Correct = false;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                resultado.Exception = ex;
                resultado.ErrorMessage = ex.Message;
                resultado.Correct = false;
                return BadRequest(resultado);
            }

        }
        [HttpGet]
        [Route("GetById/{IdProducto}")]
        public IActionResult GetById(int IdProducto)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Producto.FirstOrDefault(c => c.IdProducto == IdProducto);
                if (query != null)
                {
                    ProductoDTO productoDTO = new ProductoDTO()
                    {
                        IdProducto = query.IdProducto,
                        NombreProducto = query.NombreProducto,
                        Precio = query.Precio,
                        Categoria = query.Categoria,    
                        UrlImagen = query.UrlImagen
                    };

                    resultDTO.Object = productoDTO;
                    return Ok(resultDTO);

                }
                else
                {
                    resultDTO.ErrorMessage = "No hay registros";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);

                }
            }
            catch (Exception ex)
            {
                resultDTO.Exception = ex;
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                return BadRequest(resultDTO);

            }
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]ProductoDTO producto)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                Producto producto1 = new Producto()
                {
                    NombreProducto = producto.NombreProducto,
                    Precio = producto.Precio,
                    Categoria = producto.Categoria,
                    UrlImagen = producto.UrlImagen
                };
                _dbcontext.Producto.Add(producto1);
               int rowAffected = _dbcontext.SaveChanges();
                if (rowAffected > 0)
                {
                    resultDTO.ErrorMessage = "Registro agregado correctamente";
                    return Ok(resultDTO);
                }
                else
                {
                    resultDTO.ErrorMessage = "No hay registros";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);
                }
            }
            catch (Exception ex)
            {
                resultDTO.Exception = ex;
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                return BadRequest(resultDTO);
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ProductoDTO productoDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Producto.SingleOrDefault(c => c.IdProducto == productoDTO.IdProducto);
                if (query != null)
                {
                    query.NombreProducto = productoDTO.NombreProducto;
                    query.Precio = productoDTO.Precio;
                    query.Categoria = productoDTO.Categoria;
                    query.UrlImagen = productoDTO.UrlImagen;
                   int rowAffected = _dbcontext.SaveChanges();
                    if (rowAffected > 0)
                    {
                        resultDTO.ErrorMessage = "Registro Actualizado";
                        return Ok(resultDTO);
                    }
                    else
                    {
                        resultDTO.ErrorMessage = "Registro no actualizado";
                        resultDTO.Correct = false;
                        return BadRequest(resultDTO);
                    }
                }
                else
                {
                    resultDTO.ErrorMessage = "Registro no actualizado, ingrese IdProducto";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);

                }
            }
            catch(Exception ex)
            {
                resultDTO.Exception = ex;
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                return BadRequest(resultDTO);
            }

        }
        [HttpDelete]
        [Route("Delete/{IdProducto}")]
        public IActionResult Delete (int IdProducto)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Producto.SingleOrDefault(c => c.IdProducto == IdProducto);
                if(query != null)
                {
                    _dbcontext.Producto.Remove(query);
                   int rowAffected = _dbcontext.SaveChanges();
                    if (rowAffected >0)
                    {
                        resultDTO.ErrorMessage = "Registro eliminado";
                        return Ok(resultDTO);
                    }
                    else
                    {
                        resultDTO.ErrorMessage = "Registro no eliminado";
                        resultDTO.Correct = false;
                        return BadRequest(resultDTO);
                    }
                }
                else
                {
                    resultDTO.ErrorMessage = "Registro no eliminado, ingrese IdProducto";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);
                }
            }
            catch(Exception ex)
            {
                resultDTO.Exception = ex;
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                return BadRequest(resultDTO);

            }
        }
    }
}

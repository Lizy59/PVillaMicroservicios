using MicroserviciosCuponApi.Model;
using MicroserviciosCuponApi.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroserviciosCuponApi.Controllers
{
    public class CuponController : Controller
    {
        // GET: CuponController

        private readonly AppDbContext _dbcontext;

        public CuponController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Cupones.ToList();
                if (query != null)
                {
                    resultDTO.Objects = new List<object>();
                    foreach (var item in query)
                    {
                        CuponDTO cupon = new CuponDTO();
                        cupon.IdCupon = item.IdCupon;
                        cupon.Codigo = item.Codigo;
                        cupon.Descuento = item.Descuento;
                        cupon.CantidadMinima = item.CantidadMinima;


                        resultDTO.Objects.Add(cupon);
                    }
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
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                resultDTO.Exception = ex;
                return BadRequest(resultDTO);
            }

        }

        [HttpGet]
        [Route("GetById/{IdCupon}")]
        public IActionResult GetById(int IdCupon)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                //Se utiliza la expresion lambda para agregar el id
                var query = _dbcontext.Cupones.FirstOrDefault(c => c.IdCupon == IdCupon);
                if (query != null)
                {
                    CuponDTO cuponDTO = new CuponDTO();
                    cuponDTO.IdCupon = query.IdCupon;
                    cuponDTO.Codigo = query.Codigo;
                    cuponDTO.Descuento = query.Descuento;
                    cuponDTO.CantidadMinima = query.CantidadMinima;

                    resultDTO.Object = cuponDTO;

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
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                resultDTO.Exception = ex;
                return BadRequest(resultDTO);

            }
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] CuponDTO cupon)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                //Se crea un nuevo objeto pero del modelo que pertenece a la base de
                //datos para guardar la informacion que viene dentro de cupon
                Cupon cupon1 = new Cupon()
                {
                    Codigo = cupon.Codigo,
                    Descuento = cupon.Descuento,
                    CantidadMinima = cupon.CantidadMinima
                };
                _dbcontext.Add(cupon1);
                int rowAffected = _dbcontext.SaveChanges();
                if (rowAffected > 0)
                {
                    return Ok(resultDTO);
                }
                else
                {
                    resultDTO.ErrorMessage = "El registro no fue agregado";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);
                }

            }
            catch (Exception ex)
            {
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                resultDTO.Exception = ex;
                return BadRequest(resultDTO);
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody]CuponDTO cupon)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Cupones.FirstOrDefault(c => c.IdCupon == cupon.IdCupon);

                if (query != null)
                {
                    query.Codigo = cupon.Codigo;
                    query.Descuento = cupon.Descuento;
                    query.CantidadMinima = cupon.CantidadMinima;

                    int rowAffected = _dbcontext.SaveChanges();
                    if (rowAffected > 0)
                    {
                        resultDTO.ErrorMessage = "Registro actualizado correctamente";
                        return Ok(resultDTO);
                    }
                    else
                    {
                        resultDTO.ErrorMessage = "El registro no fue actualizado correctamente";
                        resultDTO.Correct = false;
                        return BadRequest(resultDTO);
                    }
                }
                else
                {
                    resultDTO.ErrorMessage = "Error";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);
                }

            }
            catch (Exception ex)
            {
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                resultDTO.Exception = ex;
                return BadRequest(resultDTO);
            }
            

        }
        [HttpGet]
        [Route("GetByCode/{Codigo}")]
    public IActionResult GetByCode(CuponDTO cupon)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Cupones.SingleOrDefault(c => c.Codigo == cupon.Codigo);
                if (query != null)
                {
                    CuponDTO cupon1 = new CuponDTO()
                    {
                            IdCupon = query.IdCupon,
                            Codigo = query.Codigo,
                            Descuento = query.Descuento,
                            CantidadMinima = query.CantidadMinima
                    };

                    resultDTO.Object = cupon1;
                    return Ok(resultDTO);
                }
                else
                {
                    resultDTO.ErrorMessage = "Error";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);
                }
            }
            catch (Exception ex)
            {
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                resultDTO.Exception = ex;
                return BadRequest(resultDTO);
            }

        }
        [HttpDelete]
        [Route("Delete/{IdCupon}")]
        public IActionResult Delete(int IdCupon)
        {
            ResultDTO resultDTO = new ResultDTO();
            try
            {
                var query = _dbcontext.Cupones.SingleOrDefault(c => c.IdCupon == IdCupon);
                if (query != null)
                {
                    _dbcontext.Cupones.Remove(query);
                   int rowAffected = _dbcontext.SaveChanges();
                    if(rowAffected > 0)
                    {
                        resultDTO.ErrorMessage = "Registro eliminado correctamente";
                        return Ok(resultDTO);
                    }
                    else
                    {
                        resultDTO.ErrorMessage = "El registro no fue eliminado";
                        resultDTO.Correct = false;
                        return BadRequest(resultDTO);
                    }
                }
                else
                {
                    resultDTO.ErrorMessage = "Error";
                    resultDTO.Correct = false;
                    return BadRequest(resultDTO);
                }

            }
            catch (Exception ex)
            {
                resultDTO.ErrorMessage = ex.Message;
                resultDTO.Correct = false;
                resultDTO.Exception = ex;
                return BadRequest(resultDTO);

            }
        }



    }
}

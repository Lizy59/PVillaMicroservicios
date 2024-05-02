using MicroservicioWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioWebApi.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult GetAll()
        {
            ProductoDTO productoDTO = new ProductoDTO();
            productoDTO.Productos = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Producto"]);
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();
                var result = responseTask.Result;   

                if(result.IsSuccessStatusCode) 
                {
                    var readTask = result.Content.ReadAsAsync<ResultDTO>();
                    readTask.Wait();
                    foreach (var item in readTask.Result.Objects)
                    {
                       
                        ProductoDTO resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductoDTO>(item.ToString());
                        productoDTO.Productos.Add(resultItemList);
                    }

                    return View(productoDTO);
                }
                else
                {
                    return View(productoDTO);
                }

            }
        }

        public ActionResult Form(int IdProducto)
        {
           if(IdProducto > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["Producto"]);
                    var responseTask = client.GetAsync($"GetById/{IdProducto}");
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ProductoDTO productoDTO = new ProductoDTO();
                        var readTask = result.Content.ReadAsAsync<ResultDTO>();
                        readTask.Wait();
                        var content = readTask.Result.Object;

                        productoDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductoDTO>(content.ToString());
                        return View(productoDTO);
                    }
                    else
                    {
                        ViewBag.Text = "No hay registro";
                        return PartialView("_Modal");
                    }
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Form(ProductoDTO productoDTO)
        {
            if (productoDTO.IdProducto > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["Producto"]);
                    var responseTask = client.PutAsJsonAsync<ProductoDTO>("Update", productoDTO);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El registro se actualizo correctamente";
                        return PartialView("_Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El registro no actualizo correctamente";
                        return PartialView("_Modal");
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["Producto"]);
                    var responseTask = client.PostAsJsonAsync<ProductoDTO>("Add", productoDTO);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El registro se agrego correctamente";
                        return PartialView("_Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El registro no se agrego correctamente";
                        return PartialView("_Modal");
                    }
                }
            }
        }

       
        public IActionResult Delete(int IdProducto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Producto"]);
               var response = client.DeleteAsync("Delete/"+IdProducto);
                response.Wait();
                var result = response.Result;
                if(result.IsSuccessStatusCode)
                {
                    ViewBag.Text = "El registro se ha eliminado correctamente";
                    return PartialView("_Modal");
                }
                else
                {
                    ViewBag.Text = "El registro se no eliminado";
                    return PartialView("_Modal");
                }
                
            }

        }



    }
}

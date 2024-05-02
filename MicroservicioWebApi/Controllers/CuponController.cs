using MicroservicioWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioWebApi.Controllers
{
    public class CuponController : Controller
    {
        private readonly IConfiguration _configuration;
        public CuponController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult GetAll()
        {
             CuponDTO cuponDTO = new CuponDTO();
            cuponDTO.CuponesL = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Cupon"]);
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResultDTO>();
                    readTask.Wait();
                    foreach (var item in readTask.Result.Objects)
                    {

                        CuponDTO resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<CuponDTO>(item.ToString());
                        cuponDTO.CuponesL.Add(resultItemList);
                    }

                    return View(cuponDTO);
                }
                else
                {
                    return View(cuponDTO);
                }

            }
        }

        public ActionResult Form(int IdCupon)
        {
            if (IdCupon > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["Cupon"]);
                    var responseTask = client.GetAsync($"GetById/{IdCupon}");
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                       CuponDTO cuponDTO = new CuponDTO();
                        var readTask = result.Content.ReadAsAsync<ResultDTO>();
                        readTask.Wait();
                        var content = readTask.Result.Object;

                        cuponDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<CuponDTO>(content.ToString());
                        return View(cuponDTO);
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
        public IActionResult Form(CuponDTO cupon)
        {
            if (cupon.IdCupon > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["Cupon"]);
                    var responseTask = client.PutAsJsonAsync<CuponDTO>("Update", cupon);
                    //var responseTask = client.PutAsJsonAsync<CuponDTO>("Update", cupon);
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
                    client.BaseAddress = new Uri(_configuration["Cupon"]);
                    var responseTask = client.PostAsJsonAsync<CuponDTO>("Add", cupon);
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


        public IActionResult Delete(int IdCupon)
        {
            if(IdCupon > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["Cupon"]);
                    var response = client.DeleteAsync("Delete/" + IdCupon);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
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
            else
            {
                ViewBag.Text = "No ha seleccionado un id";
                return PartialView("_Modal");
            }
        }

    }
}

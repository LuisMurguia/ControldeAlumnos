using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace PL.Controllers
{
    public class MateriaController : Controller
    {

        // GET: Materia
        public ActionResult GetAll()
        {
            
            ML.Materia resultDepartamento = new ML.Materia();
            resultDepartamento.Materias = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44310/api/");

                var responseTask = client.GetAsync("Materia");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        resultDepartamento.Materias.Add(resultItemList);
                    }
                    //return View(resultDepartamento); 
                }
            }
            return View(resultDepartamento);
        }


        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();

            ML.Result resultMateria = BL.Materia.GetAllEF();

           


            if (IdMateria == null)
            {
                return View(materia);
            }
            else
            {
                ML.Result result = BL.Materia.GetByIdLinQ(IdMateria.Value);

                if (result.Correct)
                {
                    materia.IdMateria = ((ML.Materia)result.Object).IdMateria;
                    materia.Nombre = ((ML.Materia)result.Object).Nombre;
                    materia.Costo = ((ML.Materia)result.Object).Costo;
                    return View(materia);
                }
                else
                {
                    ML.Result result1 = new ML.Result();

                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
            }


        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {

            ML.Result result = new ML.Result();
            if (materia.IdMateria == 0)
            {
                result = BL.Materia.AddEF(materia);
                ViewBag.Message = "La materia se agregó correctamente ";
            }
            else
            {
                result = BL.Materia.UpdateEF(materia);
                ViewBag.Message = "La materia se actualizó correctamente ";

            }

            if (result.Correct)
            {
                ViewBag.Message = "Se agrego correctamente la materia" + result.ErrorMessage;
            }
            else
            {
                ViewBag.Message = "No se pudo agregar correctamente la materia " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdMateria)
        {

 using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:10037/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Materia/Delete/" + IdMateria);

                deleteTask.Wait();

                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    //resultListProduct = BL.Departamento.GetAllEF();

                    return RedirectToAction("GetAll");

                }

            }

            return View("GetAll"); 
        }
    }
    }